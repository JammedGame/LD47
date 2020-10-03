using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public int Width = 10;
    public int Height = 10;
    public TileType[] Tiles;
}
