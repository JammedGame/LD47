using UnityEngine;

public class Train
{
	private readonly GameWorld world;
	private Tile tile;
	private Direction direction;

	public Train(GameWorld world, int x, int y)
	{
		this.world = world;
		this.tile = world.Tiles[x, y];
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

	public void Tick()
	{

	}
}