using System;
using UnityEngine;

public class Train
{
	private readonly GameWorld world;
	private Tile tile;
	private Tile lastTile;
	private Direction direction;
	private float progressToTile;

	public Train(GameWorld world, TrainSpawn trainSpawn)
	{
		this.world = world;
		this.tile = world.GetTile(trainSpawn.X, trainSpawn.Y) ?? throw new Exception($"Train at invalid position: [{trainSpawn.X}, {trainSpawn.Y}]");
		this.lastTile = tile;
		this.progressToTile = 1f;
		this.direction = trainSpawn.Direction;
	}

	public Direction Direction => direction;
	public GameWorld World => world;
	public float ProgressToTile => progressToTile;
	public Tile LastTile => lastTile;

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
				lastTile = tile;
				tile = value;
			}
		}
	}

	public void Tick(float dT)
	{
		if (progressToTile >= 1f)
		{
			GoToNextTile();
			progressToTile -= 1f;
		}
		progressToTile += dT;
	}

	private void GoToNextTile()
	{
		var nextTile = tile.GetAdjecentTile(direction);
		var nextDirection = nextTile.GetExitDirectionFrom(direction.Opposite());
		this.direction = nextDirection;
		Tile = nextTile;
	}
}