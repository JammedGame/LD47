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

    public TileTypeSettings(TileType tileType, TileType nextTileType, Direction leftExit, Direction rightExit, Direction topExit, Direction bottomExit)
    {
        TileType = tileType;
        NextTileType = nextTileType;
        LeftExit = leftExit;
        RightExit = rightExit;
        TopExit = topExit;
        BottomExit = bottomExit;
    }

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

public static class TileTypeSettingsUtil
{
    private static readonly TileTypeSettings[] settingsPerTile = new TileTypeSettings[]
    {
        new TileTypeSettings(TileType.Turn, default, Direction.Bottom, default, default, Direction.Left),
    };
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
