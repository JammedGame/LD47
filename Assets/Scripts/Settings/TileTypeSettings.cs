using System;
using UnityEngine;

[Serializable]
public class TileTypeSettings
{
	public TileType TileType;
	public TileType NextTileType;
	public Direction LeftExit;
	public Direction RightExit;
	public Direction TopExit;
	public Direction BottomExit;

	public Direction GetExitDirection(Direction enter)
	{
		switch (enter)
		{
			case Direction.Left: return LeftExit;
			case Direction.Right: return RightExit;
			case Direction.Bottom: return BottomExit;
			case Direction.Top: return TopExit;
			default: return Direction.Undefined;
		}
	}
}

[Serializable]
public class TileIconSettings
{
	public TileType TileType;
	public Texture Icon;
	public Rotation Rotation;
}

public enum Direction
{
	Undefined = 0,
	Left = 1,
	Right = 2,
	Top = 3,
	Bottom = 4
}

public enum Rotation
{
	Rotate0 = 0,
	Rotate90 = 1,
	Rotatate180 = 2,
	Rotatate270 = 3
}

public static class RotationExtensions
{
	public static float ToAngle(this Rotation r)
	{
		switch (r)
		{
			case Rotation.Rotate0:
				return 0f;
			case Rotation.Rotate90:
				return 90f;
			case Rotation.Rotatate180:
				return 180f;
			case Rotation.Rotatate270:
				return 270f;
			default:
				throw new ArgumentOutOfRangeException(nameof(r), r, null);
		}
	}
}