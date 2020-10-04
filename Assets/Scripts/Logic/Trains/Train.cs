using System;
using UnityEngine;

public class Train
{
	private readonly GameWorld world;
	private Tile tile;
	private Direction direction;

	public Train(GameWorld world, TrainSpawn trainSpawn)
	{
		this.world = world;
		this.tile = world.GetTile(trainSpawn.X, trainSpawn.Y) ?? throw new Exception($"Train at invalid position: [{trainSpawn.X}, {trainSpawn.Y}]");
		this.direction = trainSpawn.Direction;
	}

	public Direction Direction => direction;
	public GameWorld World => world;

	public Tile Tile
	{
		get => tile;
		set
		{
			if (value == null)
			{
				Debug.LogError("Tried to set tile to null");
			}

			if (tile != value)
			{
				tile = value;
				// something something.
			}
		}
	}

	public void Tick(float dT)
	{
		dummyTimer += dT;
		if (dummyTimer > 1f)
		{
			GoToNextTile();
			dummyTimer -= 1f;
		}
	}

	private void GoToNextTile()
	{
		var nextTile = tile.GetAdjecentTile(direction);
		var nextDirection = nextTile.GetExitDirectionFrom(direction.Opposite());
		this.direction = nextDirection;
		this.tile = nextTile;
	}

	float dummyTimer;
}