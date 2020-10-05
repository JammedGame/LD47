using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
	public readonly GameWorld World;
	public readonly int X, Y;
	public CargoSpawner CargoSpawner { get; private set; }

	public Tile(GameWorld world, int x, int y, TileType type)
	{
		this.World = world;
		TileType = type;
		X = x;
		Y = y;
	}

	private TileTypeSettings tileTypeSettings => GameSettings.Instance.GetSettings(TileType);


	public TileType TileType { get; private set; }

	public Tile GetAdjecentTile(Direction direction)
	{
		switch (direction)
		{
			case Direction.Left: return World.GetTile(X - 1, Y);
			case Direction.Right: return World.GetTile(X + 1, Y);
			case Direction.Top: return World.GetTile(X, Y - 1);
			case Direction.Bottom: return World.GetTile(X, Y + 1);
			default: throw new Exception($"Undefined direction: {direction}");
		}
	}

	public void AddCargoSpawn(CargoSpawn spawn)
	{
		if (CargoSpawner == null)
			CargoSpawner = new CargoSpawner(this);

		CargoSpawner.AddSpawn(spawn);
	}

	public bool CanChange => tileTypeSettings.NextTileType != TileType.Undefined;

	public void OnClicked()
	{
		if (CanChange)
		{
			TileType = tileTypeSettings.NextTileType;
			SoundManager.Instance.PlaySoundTrackSwitch();
		}
	}

	public Direction GetExitDirectionFrom(Direction enterDirection)
	{
		return tileTypeSettings.GetExitDirection(enterDirection);
	}

	public Vector3 GetCorner(Direction enterDirection, Direction exitDirection)
	{
		var pos = this.GetPosition3D();
		if (enterDirection == Direction.Left || exitDirection == Direction.Left) pos.x -= 0.5f;
		if (enterDirection == Direction.Right || exitDirection == Direction.Right) pos.x += 0.5f;
		if (enterDirection == Direction.Top || exitDirection == Direction.Top) pos.y += 0.5f;
		if (enterDirection == Direction.Bottom || exitDirection == Direction.Bottom) pos.y -= 0.5f;
		return pos;
	}

	public (Texture texture, Rotation rotation, Texture overlay) LoadTexture()
	{
		return GameSettings.Instance.GetTexture(TileType);
	}
}