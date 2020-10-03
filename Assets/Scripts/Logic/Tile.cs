public class Tile
{
    public readonly int X, Y;
    private TileType tileType;

    public TileType TileType => tileType;
    
    public Tile(int x, int y, TileType type)
    {
        this.tileType = type;
        this.X = x;
        this.Y = y;
    }
}