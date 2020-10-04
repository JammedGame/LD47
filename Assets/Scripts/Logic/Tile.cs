using System;
using UnityEngine;

public class Tile
{
    public readonly int X, Y;
    private TileType tileType;
    private readonly GameWorld world;
    private TileTypeSettings tileTypeSettings => GameSettings.Instance.GetSettings(tileType);
    private bool enabled;

    public TileType TileType => tileType;
    public bool Enabled => enabled;

    public Tile GetAdjecentTile(Direction direction)
    {
        switch(direction)
        {
            case Direction.Left: return world.GetTile(X - 1, Y);
            case Direction.Right: return world.GetTile(X + 1, Y);
            case Direction.Top: return world.GetTile(X, Y - 1);
            case Direction.Bottom: return world.GetTile(X, Y + 1);
            default: throw new System.Exception($"Undefined direction: {direction}");
        }
    }

	public void OnClicked()
	{
        if (tileTypeSettings.NextTileType != TileType.Undefined)
        {
            tileType = tileTypeSettings.NextTileType;
        }
	}

	public Direction GetExitDirectionFrom(Direction enterDirection)
	{
        return tileTypeSettings.GetExitDirection(enterDirection);
	}

	public Tile(GameWorld world, int x, int y, TileType type)
    {
        this.world = world;
        this.tileType = type;
        this.X = x;
        this.Y = y;
        this.enabled = true;
    }

    public (Texture texture, Rotation rotation) LoadTexture()
	{
        return GameSettings.Instance.GetTexture(tileType);
	}
}