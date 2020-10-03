using UnityEngine;

public enum TileType
{
	Undefined = 18,
	Rail = 0,
	Turn = 1,
	ThreewayType1_1Enabled = 2,
	ThreewayType1_1Disabled = 3,
	ThreewayType1_2Enabled = 4,
	ThreewayType1_2Disabled = 5,
	ThreewayType2_1Enabled = 6,
	ThreewayType2_1Disabled = 7,
	ThreewayType2_2Enabled = 8,
	ThreewayType2_2Disabled = 9,
	ThreewayType3_1Enabled = 10,
	ThreewayType3_1Disabled = 11,
	ThreewayType3_2Enabled = 12,
	ThreewayType3_2Disabled = 13,
	FourwayMerge = 14,
	FourwayOverpass = 15,
	FourwaySwitchEnabled = 16,
	FourwatSwitchDisabled = 17
}

public static class TileTypeExtensions
{
	public static Texture LoadTexture(this TileType type)
	{
		string indexString = ((int)type).ToString("D2");
		return Resources.Load<Texture>($"Textures/r{(indexString)}");
	}
}