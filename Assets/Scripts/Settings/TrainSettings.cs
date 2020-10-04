﻿using System;
using UnityEngine;

[Serializable]
public class TrainTypeSettings
{
	public TrainType Type;
	public Texture LocomotiveIcon;
	public Texture CarIcon;
	public float DefaultSpeed;
	public int DefaultInitialCars;
}

[Serializable]
public class TrainColorSettings
{
	public TrainColor TrainColor;
	public Color Color;
}