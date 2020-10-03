using UnityEngine;

public enum TileType
{
	Undefined = 18,
	Rail = 0,
	Turn = 1,
	ThreewayType1_1 = 2,
	ThreewayType1_2 = 4,
	ThreewayType2_1 = 6,
	ThreewayType2_2 = 8,
	ThreewayType3_1 = 10,
	ThreewayType3_2 = 12,
	FourwayMerge = 14,
	FourwayOverpass = 15,
	FourwaySwitch = 16,
	FourwatSwitch = 17
}

public static class TileTypeExtensions
{
	public static Texture LoadTexture(this TileType type)
	{
		string indexString = ((int)type).ToString("D2");
		return Resources.Load<Texture>($"Textures/r{(indexString)}");
	}
}