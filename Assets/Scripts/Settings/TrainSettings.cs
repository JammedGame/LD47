using System;
using UnityEngine;

[Serializable]
public class TrainTypeSettings
{
	public TrainType Type;
	public float DefaultSpeed;
}

[Serializable]
public class TrainColorSettings
{
	public TrainColor TrainColor;
	public Color Color;
	public Texture LocomotiveIcon;
	public Texture CarIcon;
}