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

    public Texture LoadTexture()
	{
        int artIndex = (int)this.tileType + ((this.enabled) ? 0 : 1);
		string indexString = artIndex.ToString("D2");
		return Resources.Load<Texture>($"Textures/r{(indexString)}");
	}
}