using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
	public int Width = 10;
	public int Height = 10;
	public float VictoryAfterSeconds;
	public int VictoryAfterCargoCollected;
	public float DefeatAfterSeconds;
	public int DefeatAfterCargoOverflow;
	public float DefaultCargoDespawnAfterSeconds;
	public int DefeatAfterCargoDespawned;
	[Table] public List<TrainSpawn> TrainSpawns;
	[Table] public List<CargoSpawn> CargoSpawns;
	public TileType[] Tiles;

	public TileData GetTile(int index)
	{
		return new TileData
		{
			Type = Tiles[index], X = index % Width, Y = index / Width
		};
	}

	public void SetTileType(int index, TileType type)
	{
		Tiles[index] = type;
	}
}

public struct TileData
{
	public int X;
	public int Y;
	public TileType Type;
}

[Serializable]
public struct TrainSpawn
{
	public int X;
	public int Y;
	public Direction Direction;
	public TrainType Type;
	public TrainColor Color;
	public int InitialCars;
}

public enum TrainType
{
	Undefined = 0,
	Slow = 1,
	Fast = 2,
	FastButLikeEvenMoreFasterYouKnow = 3
}

public enum TrainColor
{
	Undefined = 0,
	Cyan = 1,
	Magenta = 2,
	Yellow = 3,
	BioShockSepiaAesthetic = 4
}

[Serializable]
public struct CargoSpawn
{
	public int X;
	public int Y;
	public float SpawnsAtSeconds;
	public float DespawnsAfterSeconds;
	public CargoType Type;
	public TrainColor Color;
}

public enum CargoType
{
	Default = 0,
	NewCar = 1
}