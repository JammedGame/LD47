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
	CargoStation_R90,
	River00,
	River01,
	River02,
	River03,
	River04,
	River05,
	River06,
	River07,
	River08,
	River09,
	River10,
	River11,
	River12,
	Park1,
	Park1_R180,
	Park2,
	Park2_R180,
	House1,
	House2,
	House3,
	House4,
	BigBuilding1_1,
	BigBuilding1_2,
	BigBuilding1_3,
	BigBuilding1_4,
	BigBuilding2_1,
	BigBuilding2_2,
	BigBuilding2_3,
	BigBuilding2_4,
	Bridge,
	Bridge_R90,
	FourwayOverpass_R90
	FourwayMerge_R90 = 300,
	FourwayMerge_R180,
	FourwayMerge_R270,
	FourwaySwitch_R90
}

public static class TileTypeExtensions
{
	public static (Texture tex, Rotation rotate, Texture overlay) LoadTexture(this TileType type)
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

			case TileType.Park1_R180: return (TileType.Park1, Rotation.Rotate180);
			case TileType.Park2_R180: return (TileType.Park2, Rotation.Rotate180);

			case TileType.Bridge_R90: return (TileType.Bridge, Rotation.Rotate90);
			case TileType.FourwayOverpass_R90: return (TileType.FourwayOverpass, Rotation.Rotate90);

			case TileType.FourwayMerge_R90: return (TileType.FourwayMerge, Rotation.Rotate90);
			case TileType.FourwayMerge_R180: return (TileType.FourwayMerge, Rotation.Rotate180);
			case TileType.FourwayMerge_R270: return (TileType.FourwayMerge, Rotation.Rotate270);

			case TileType.FourwaySwitch_R90: return (TileType.FourwaySwitch, Rotation.Rotate90);

			default: return (type, Rotation.Rotate0);
		}
	}
}