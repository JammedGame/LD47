using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
	public Vector3 InitialCameraPan;
	public float InitialCameraZoom = 5f;
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

	public int GetInitialCars()
	{
		if (InitialCars > 0) return InitialCars;

		var type = Type;
		return GameSettings.Instance.SettingsPerTrainType.Find(s => s.Type == type)?.DefaultInitialCars ?? 0;
	}
}

public enum TrainType
{
	Undefined = 0,
	Slow = 1,
	Fast = 2,
	ExtraSlow = 3
}

public static class TrainTypeExtensions
{
	public static float GetSpeed(this TrainType trainType)
	{
		return GameSettings.Instance.SettingsPerTrainType.Find(s => s.Type == trainType)?.DefaultSpeed ?? 1f;
	}
}

public enum TrainColor
{
	Undefined = 0,
	Cyan = 1,
	Magenta = 2,
	Yellow = 3,
	BioShockSepiaAesthetic = 4
}

public static class TrainColorExtensions
{
	public static Color ToColor(this TrainColor trainColor)
	{
		return GameSettings.Instance.SettingsPerTrainColor.Find(s => s.TrainColor == trainColor)?.Color ?? Color.black;
	}

	public static Texture LoadLocomotiveTexture(this TrainColor trainColor)
	{
		return GameSettings.Instance.SettingsPerTrainColor.Find(s => s.TrainColor == trainColor)?.LocomotiveIcon;
	}

	public static Texture LoadCarTexture(this TrainColor trainColor)
	{
		return GameSettings.Instance.SettingsPerTrainColor.Find(s => s.TrainColor == trainColor)?.CarIcon;
	}
}

[Serializable]
public struct CargoSpawn
{
	public int X;
	public int Y;
	public float SpawnsAtSeconds;
	public float DespawnsAfterSeconds;
	public TrainColor Color;
}