using System;
using UnityEngine;

[Serializable]
public class TrainTypeSettings
{
	public TrainType Type;
	public Texture Icon;
	public float DefaultSpeed;
	public int DefaultInitialCars;
}

[Serializable]
public class TrainColorSettings
{
	public TrainColor TrainColor;
	public Color Color;
}

public static class TrainColorExtensions
{
	public static Color ToColor(this TrainColor trainColor)
	{
		return GameSettings.Instance.SettingsPerTrainColor.Find(s => s.TrainColor == trainColor)?.Color ?? Color.black;
	}
}