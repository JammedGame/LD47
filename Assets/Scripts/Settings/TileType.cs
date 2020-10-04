using UnityEngine;

public enum TileType
{
	Undefined = 0,
	Rail = 1,
	Rail_R90 = 2,
	Turn = 3,
	Turn_R90 = 20,
	Turn_R180 = 21,
	Turn_R270 = 22,
	ThreewayType1_1 = 4,
	ThreewayType1_2 = 5,
	ThreewayType2_1 = 6,
	ThreewayType2_2 = 8,
	ThreewayType3_1 = 10,
	ThreewayType3_2 = 12,
	FourwayMerge = 14,
	FourwayOverpass = 15,
	FourwaySwitch = 16,
	ThreewayType1_1_R90 = 100,
	ThreewayType1_2_R90,
	ThreewayType2_1_R90,
	ThreewayType2_2_R90,
	ThreewayType3_1_R90,
	ThreewayType3_2_R90,
	ThreewayType1_1_R180,
	ThreewayType1_2_R180,
	ThreewayType2_1_R180,
	ThreewayType2_2_R180,
	ThreewayType3_1_R180,
	ThreewayType3_2_R180,
	ThreewayType1_1_R270,
	ThreewayType1_2_R270,
	ThreewayType2_1_R270,
	ThreewayType2_2_R270,
	ThreewayType3_1_R270,
	ThreewayType3_2_R270,
	Terminus = 200,
	Terminus_R90,
	CargoStation,
	CargoStation_R90
}

public static class TileTypeExtensions
{
	public static (Texture tex, Rotation rotate) LoadTexture(this TileType type)
	{
		return GameSettings.Instance.GetTexture(type);
	}

	public static (TileType source, Rotation rotation) GetRotation(this TileType type)
	{
		switch (type)
		{
			case TileType.Rail_R90: return (TileType.Rail, Rotation.Rotate90);

			case TileType.Turn_R90: return (TileType.Turn, Rotation.Rotate90);
			case TileType.Turn_R180: return (TileType.Turn, Rotation.Rotate180);
			case TileType.Turn_R270: return (TileType.Turn, Rotation.Rotate270);

			case TileType.ThreewayType1_1_R90: return (TileType.ThreewayType1_1, Rotation.Rotate90);
			case TileType.ThreewayType1_2_R90: return (TileType.ThreewayType1_2, Rotation.Rotate90);
			case TileType.ThreewayType2_1_R90: return (TileType.ThreewayType2_1, Rotation.Rotate90);
			case TileType.ThreewayType2_2_R90: return (TileType.ThreewayType2_2, Rotation.Rotate90);
			case TileType.ThreewayType3_1_R90: return (TileType.ThreewayType3_1, Rotation.Rotate90);
			case TileType.ThreewayType3_2_R90: return (TileType.ThreewayType3_2, Rotation.Rotate90);

			case TileType.ThreewayType1_1_R180: return (TileType.ThreewayType1_1, Rotation.Rotate180);
			case TileType.ThreewayType1_2_R180: return (TileType.ThreewayType1_2, Rotation.Rotate180);
			case TileType.ThreewayType2_1_R180: return (TileType.ThreewayType2_1, Rotation.Rotate180);
			case TileType.ThreewayType2_2_R180: return (TileType.ThreewayType2_2, Rotation.Rotate180);
			case TileType.ThreewayType3_1_R180: return (TileType.ThreewayType3_1, Rotation.Rotate180);
			case TileType.ThreewayType3_2_R180: return (TileType.ThreewayType3_2, Rotation.Rotate180);

			case TileType.ThreewayType1_1_R270: return (TileType.ThreewayType1_1, Rotation.Rotate270);
			case TileType.ThreewayType1_2_R270: return (TileType.ThreewayType1_2, Rotation.Rotate270);
			case TileType.ThreewayType2_1_R270: return (TileType.ThreewayType2_1, Rotation.Rotate270);
			case TileType.ThreewayType2_2_R270: return (TileType.ThreewayType2_2, Rotation.Rotate270);
			case TileType.ThreewayType3_1_R270: return (TileType.ThreewayType3_1, Rotation.Rotate270);
			case TileType.ThreewayType3_2_R270: return (TileType.ThreewayType3_2, Rotation.Rotate270);

			case TileType.Terminus_R90: return (TileType.Terminus, Rotation.Rotate90);
			case TileType.CargoStation_R90: return (TileType.CargoStation, Rotation.Rotate90);

			default: return (type, Rotation.Rotate0);
		}
	}
}