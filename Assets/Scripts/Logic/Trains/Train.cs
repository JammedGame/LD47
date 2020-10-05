using System;
using System.Collections.Generic;
using UnityEngine;

public class Train
{
	public const float collisionRadius = 0.75f;
	private readonly GameWorld world;
	private Tile tile;
	private Direction direction;
	private Direction tileEnterDirection;
	private float progressInsideTile;
	private readonly PositionStateHistory positionHistory;
	private TrainType type;
	private TrainColor color;
	private int cars;
	private float speed;
	private readonly List<float> scheduledCars = new List<float>();

	public TrainType Type => type;
	public TrainColor Color => color;
	public int Cars => cars;
	public float Speed => speed;
	public int TicksBetweenCars => (int) (1 / (Speed * GameTicker.DeterministicTick));

	public PositionState GetSnapshot() => new PositionState()
	{
		ExitDirection = direction,
		EnterDirection = tileEnterDirection,
		Tile = tile,
		ProgressInTile = progressInsideTile
	};

	public Train(GameWorld world, TrainSpawn trainSpawn)
	{
		this.world = world;
		this.tile = world.GetTile(trainSpawn.X, trainSpawn.Y) ?? throw new Exception($"Train at invalid position: [{trainSpawn.X}, {trainSpawn.Y}]");
		this.progressInsideTile = 0.5f;
		this.direction = trainSpawn.Direction;
		this.tileEnterDirection = direction.Opposite();
		this.positionHistory = new PositionStateHistory(1000, GetSnapshot());
		this.type = trainSpawn.Type;
		this.color = trainSpawn.Color;
		this.cars = trainSpawn.GetInitialCars();
		this.speed = trainSpawn.Type.GetSpeed();
	}

	public PositionState GetSnapshotFromHistory(int i)
	{
		var index = Mathf.Min(i, positionHistory.Count - 1);
		return positionHistory.Get(index);
	}

	public PositionState GetLocmotiveOrWagonState(int index) // 0 locomotive, rest wagons
	{
		return GetSnapshotFromHistory(index * TicksBetweenCars);
	}

	public Direction Direction => direction;
	public Direction TileEnterDirection => tileEnterDirection;
	public GameWorld World => world;
	public float ProgressInsideTile => progressInsideTile;

	public Tile Tile => tile;

	public void Tick(float dT)
	{
		// check collsion
		if (GetDistToNearestCollider(GetSnapshot()) < collisionRadius)
		{
			return;
		}

		// check if entered a new tile
		progressInsideTile += dT * speed;
		if (progressInsideTile >= 1f)
		{
			if (EnterNextTile())
			{
				progressInsideTile -= 1f;
			}
			else
			{
				progressInsideTile = 1f;
				return; // skips snapshot
			}
		}

		positionHistory.Add(GetSnapshot());

		if (scheduledCars.Count > 0)
		{
			scheduledCars[0] -= dT;
			if (scheduledCars[0] <= 0)
			{
				if (scheduledCars.Count > 1) scheduledCars[1] += scheduledCars[0];
				scheduledCars.RemoveAt(0);
				cars++;
			}
		}
	}

	public float GetDistToNearestCollider(PositionState state)
	{
		var snap = GetSnapshot();
		var trainPos = snap.GetPosition();
		var direction = snap.GetDirection();
		var minDist = float.MaxValue;
		var isOverpass = snap.IsOverpass();
		foreach(var train in world.AllTrains)
		{
			for(int i = 0; i < train.cars + 1; i++)
			{
				if (train == this && i == 0) continue;
				if (train == this && positionHistory.Count < TicksBetweenCars) continue;

				var posSnap = train.GetLocmotiveOrWagonState(i);
				var colliderPos = posSnap.GetPosition();
				var dirToCollider = colliderPos - trainPos;
				if (Vector3.Dot(dirToCollider, direction) < 0)
					continue;

				var dist = Vector3.Distance(trainPos, colliderPos);
				if (dist < minDist && posSnap.IsOverpass() == isOverpass)
				{
					minDist = dist;
				}
			}
		}

		return minDist;
	}

	private bool EnterNextTile()
	{
		var oldTile = tile;
		var nextTile = tile.GetAdjecentTile(direction);

		// update state.
		this.tileEnterDirection = direction.Opposite();
		this.direction = nextTile.GetExitDirectionFrom(tileEnterDirection);
		this.tile = nextTile;

		if (oldTile.CargoSpawner != null)
		{
			var carsAdded = oldTile.CargoSpawner.RemoveAllFor(color);
			for(int i = 0; i < carsAdded; i++)
			{
				scheduledCars.Add(1f / Speed);
			}
		}

		return true;
	}
}

public struct PositionState
{
	public Tile Tile;
	public float ProgressInTile;
	public Direction EnterDirection;
	public Direction ExitDirection;

    public Vector3 GetPosition()
    {
		var tileEnterPos = Tile.GetPosition3D(EnterDirection);
		var tileExitPos = Tile.GetPosition3D(ExitDirection);

		if (EnterDirection == ExitDirection.Opposite())
		{
			return Vector3.Lerp(tileEnterPos, tileExitPos, ProgressInTile);
		}
		else
		{
			var cornerPos = Tile.GetCorner(EnterDirection, ExitDirection);
			var enterPosDir = tileEnterPos - cornerPos;
			var exitPosDir = tileExitPos - cornerPos;
			return cornerPos + Vector3.Slerp(enterPosDir, exitPosDir, ProgressInTile).normalized * 0.5f;
		}
    }

	public Vector2 GetDirection()
	{
		var angle = GetAngle() / 180f * Mathf.PI;
		return new Vector3(-Mathf.Sin(angle), Mathf.Cos(angle), 0);
	}

    internal float GetAngle()
    {
		var enterAngle = EnterDirection.Opposite().ToAngle();
		var exitRotation = ExitDirection.ToAngle();
		return Mathf.LerpAngle(enterAngle, exitRotation, ProgressInTile);
    }

	public bool IsOverpass()
	{
		var tile = Tile;
		if (tile.TileType == TileType.FourwayOverpass_R90 && (EnterDirection == Direction.Left || ExitDirection == Direction.Left)) return true;
		if (tile.TileType == TileType.FourwayOverpass && (EnterDirection == Direction.Top || ExitDirection == Direction.Top)) return true;

		if (ProgressInTile < 0.5f)
		{
			var previousTile = Tile.GetAdjecentTile(EnterDirection);
			if (previousTile.TileType == TileType.FourwayOverpass_R90 && (EnterDirection == Direction.Left || EnterDirection == Direction.Left)) return true;
			if (previousTile.TileType == TileType.FourwayOverpass && (EnterDirection == Direction.Top || EnterDirection == Direction.Bottom)) return true;
		}

		if (ProgressInTile > 0.5f)
		{
			var nextTile = Tile.GetAdjecentTile(ExitDirection);
			if (nextTile.TileType == TileType.FourwayOverpass_R90 && (ExitDirection == Direction.Left || ExitDirection == Direction.Right)) return true;
			if (nextTile.TileType == TileType.FourwayOverpass && (ExitDirection == Direction.Top || ExitDirection == Direction.Bottom)) return true;
		}

		return false;
	}
}

public class PositionStateHistory
{
	int bufferSize;
	readonly PositionState[] buffer;

	int currentIndex = 0;
	int currentSize = 0;

	public int Count => currentSize;
	public int MaxSize => bufferSize;

	public PositionStateHistory(int bufferSize, PositionState initial)
	{
		this.bufferSize = bufferSize;
		buffer = new PositionState[bufferSize];
		Add(initial);
	}

	public void Add(PositionState state)
	{
		if (state.Tile == null)
			throw new NullReferenceException();

		buffer[currentIndex] = state;
		currentIndex++;
		if (currentSize < bufferSize) { currentSize++; }

		// round buffer
		if (currentIndex >= bufferSize)
		{
			currentIndex = 0;
		}
	}

	public PositionState Get(int ticksAgo)
	{
		if (ticksAgo >= currentSize)
			throw new IndexOutOfRangeException();

		var index = currentIndex - 1 - ticksAgo;
		if (index < 0)
			index += bufferSize;
		return buffer[index];
	}

    public bool OccupiesTile(Tile nextTile, int v)
    {
		var current = currentIndex;
		for(int i = 0; i < v; i++)
		{
			if (buffer[current].Tile == nextTile)
				return true;

			if (current == 0)
				current = bufferSize - 1;
			else
				current--;
		}

		return false;
    }
}