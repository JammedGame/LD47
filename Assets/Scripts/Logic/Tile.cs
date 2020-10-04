using System;
using UnityEngine;

public class Tile
{
    public readonly int X, Y;
    private TileType tileType;
    private readonly GameWorld world;
    private readonly TileTypeSettings tileTypeSettings;
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
        this.tileTypeSettings = GameSettings.Instance.GetSettings(tileType);
    }

    public (Texture texture, Rotation rotation) LoadTexture()
	{
        return GameSettings.Instance.GetTexture(tileType);
	}
}