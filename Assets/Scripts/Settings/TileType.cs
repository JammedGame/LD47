using UnityEngine;

public enum TileType
{
	Undefined = 0,
	Rail = 1
}

public static class TileTypeExtensions
{
	public static Texture LoadTexture(this TileType type)
	{
		return Resources.Load<Texture>($"Textures/Tile{type}");
	}
}