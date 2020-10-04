using System;
using System.Collections.Generic;
using UnityEngine;

public class Train
{
	private readonly GameWorld world;
	private Tile tile;
	private Direction direction;
	private Direction tileEnterDirection;
	private float progressInsideTile;
	private readonly PositionStateHistory positionHistory;

	public PositionState GetSnapshot() => new PositionState()
	{
		Direction = direction,
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
		this.positionHistory = new PositionStateHistory(200, GetSnapshot());
	}

	public Direction Direction => direction;
	public Direction TileEnterDirection => tileEnterDirection;
	public GameWorld World => world;
	public float ProgressInsideTile => progressInsideTile;

	public Tile Tile => tile;

	public void Tick(float dT)
	{
		positionHistory.Add(GetSnapshot());

		// check if entered a new tile
		progressInsideTile += dT;
		if (progressInsideTile >= 1f)
		{
			GoToNextTile();
			progressInsideTile -= 1f;
		}
	}

	private void GoToNextTile()
	{
		var nextTile = tile.GetAdjecentTile(direction);
		var nextDirection = nextTile.GetExitDirectionFrom(direction.Opposite());

		// update state.
		this.tileEnterDirection = direction.Opposite();
		this.direction = nextDirection;
		this.tile = nextTile;
	}
}

public struct PositionState
{
	public Tile Tile;
	public float ProgressInTile;
	public Direction Direction;
	public Direction EnterDirection;
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
		buffer[currentIndex] = state;
		currentIndex++;
		if (currentSize < bufferSize) { currentSize++; }

		// round buffer
		if (currentIndex >= bufferSize)
		{
			currentIndex = 0;
		}
	}

	public PositionState GetIndex(int ticksAgo)
	{
		if (ticksAgo >= currentSize)
			throw new IndexOutOfRangeException();

		var index = currentIndex - ticksAgo;
		if (index < 0)
			index += bufferSize;
		return buffer[index];
	}
}