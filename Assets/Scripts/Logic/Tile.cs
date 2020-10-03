using UnityEngine;

public class Tile
{
    public readonly int X, Y;
    private TileType tileType;
    private bool enabled;

    public TileType TileType => tileType;
    public bool Enabled => enabled;
    
    public Tile(int x, int y, TileType type)
    {
        this.tileType = type;
        this.X = x;
        this.Y = y;
        this.enabled = true;
    }

    public (Texture texture, Rotation rotation) LoadTexture()
	{
        return GameSettings.Instance.GetTexture(tileType, Enabled);
	}
}