using System;
using System.Collections;
using System.Collections.Generic;
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
        switch(enter)
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
    Rotate180 = 2,
    Rotate270 = 3
}

public static class DirectionUtil
{
    public static Direction Rotate(this Direction direction, Rotation rotate)
    {
        switch((rotate, direction))
        {
            case (Rotation.Rotate90, Direction.Left): return Direction.Top;
            case (Rotation.Rotate90, Direction.Top): return Direction.Right;
            case (Rotation.Rotate90, Direction.Right): return Direction.Bottom;
            case (Rotation.Rotate90, Direction.Bottom): return Direction.Left;

            case (Rotation.Rotate180, Direction.Left): return Direction.Right;
            case (Rotation.Rotate180, Direction.Top): return Direction.Bottom;
            case (Rotation.Rotate180, Direction.Right): return Direction.Left;
            case (Rotation.Rotate180, Direction.Bottom): return Direction.Top;

            case (Rotation.Rotate270, Direction.Left): return Direction.Bottom;
            case (Rotation.Rotate270, Direction.Top): return Direction.Left;
            case (Rotation.Rotate270, Direction.Right): return Direction.Top;
            case (Rotation.Rotate270, Direction.Bottom): return Direction.Right;
        }

        return direction; // no change
    }
}
