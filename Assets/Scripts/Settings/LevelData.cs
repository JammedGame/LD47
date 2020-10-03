using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
	public int Width = 10;
	public int Height = 10;
	public TileType[] Tiles;

	public TileData GetTile(int index)
	{
		return new TileData
		{
			Type = Tiles[index], X = index % Width, Y = index / Width
		};
	}
}

public struct TileData
{
	public int X;
	public int Y;
	public TileType Type;
}