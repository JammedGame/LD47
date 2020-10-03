using UnityEngine;

public enum TileType
{
	Undefined = 0,
	Rail = 1,
	Rail_R90 = 2,
	Turn = 3,
	ThreewayType1_1 = 4,
	ThreewayType1_2 = 5,
	ThreewayType2_1 = 6,
	ThreewayType2_2 = 8,
	ThreewayType3_1 = 10,
	ThreewayType3_2 = 12,
	FourwayMerge = 14,
	FourwayOverpass = 15,
	FourwaySwitch = 16
}

public static class TileTypeExtensions
{
	public static (Texture tex, Rotation rotate) LoadTexture(this TileType type)
	{ 
		return GameSettings.Instance.GetTexture(type);
	}

	public static (TileType source, Rotation rotation) GetRotation(this TileType type)
	{
		switch(type)
		{
			case TileType.Rail_R90: return(TileType.Rail, Rotation.Rotate90);

			default: return(type, Rotation.Rotate0);
		}
	}
}