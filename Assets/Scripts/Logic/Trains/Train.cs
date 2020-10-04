using System;
using UnityEngine;

public class Train
{
	private readonly GameWorld world;
	private Tile tile;
	private Tile lastTile;
	private Direction direction;
	private Direction tileEnterDirection;
	private float progressInsideTile;

	public Train(GameWorld world, TrainSpawn trainSpawn)
	{
		this.world = world;
		this.tile = world.GetTile(trainSpawn.X, trainSpawn.Y) ?? throw new Exception($"Train at invalid position: [{trainSpawn.X}, {trainSpawn.Y}]");
		this.lastTile = tile;
		this.progressInsideTile = 0.5f;
		this.direction = trainSpawn.Direction;
		this.tileEnterDirection = direction.Opposite();
	}

	public Direction Direction => direction;
	public Direction TileEnterDirection => tileEnterDirection;
	public GameWorld World => world;
	public float ProgressInsideTile => progressInsideTile;
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
		if (progressInsideTile >= 1f)
		{
			GoToNextTile();
			progressInsideTile -= 1f;
		}
		progressInsideTile += dT;
	}

	private void GoToNextTile()
	{
		var nextTile = tile.GetAdjecentTile(direction);
		var nextDirection = nextTile.GetExitDirectionFrom(direction.Opposite());
		this.tileEnterDirection = direction.Opposite();
		this.direction = nextDirection;
		Tile = nextTile;
	}
}