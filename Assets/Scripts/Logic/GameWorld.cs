using System;
using System.Collections.Generic;

public class GameWorld
{
	public readonly List<CargoSpawner> AllCargoSpawners = new List<CargoSpawner>();
	public readonly LevelData LevelData;
	private readonly Tile[,] tiles;
	public List<Train> AllTrains { get; } = new List<Train>();
	public float SecondsElapsed { get; private set; }
	public readonly Dictionary<TrainColor, int> NeededScore = new Dictionary<TrainColor, int>();
	public readonly Dictionary<TrainColor, int> CollectedCarsScore = new Dictionary<TrainColor, int>();
	public bool VictoryDeclared { get; private set; }
	public bool DefeatDeclared { get; private set; }

	public event Action OnScoreUpdated;
	public event Action OnVictory;
	public event Action OnDefeat;

	public GameWorld(LevelData levelData)
	{
		SecondsElapsed = 0f;

		LevelData = levelData;

		tiles = new Tile[levelData.Width, levelData.Height];
		for (var i = 0; i < levelData.Width; i++)
		for (var j = 0; j < levelData.Height; j++)
		{
			var tileType = levelData.Tiles[i + j * levelData.Width];
			var tile = new Tile(this, i, j, tileType);
			tiles[i, j] = tile;
		}

		foreach (var trainSpawn in levelData.TrainSpawns)
			AllTrains.Add(new Train(this, trainSpawn));

		foreach (var cargoSpawn in levelData.CargoSpawns)
		{
			var tile = tiles[cargoSpawn.X, cargoSpawn.Y];
			tile.AddCargoSpawn(cargoSpawn);

			NeededScore.TryGetValue(cargoSpawn.Color, out int currentScore);
			NeededScore[cargoSpawn.Color] = currentScore + 1;
		}
	}

	public void CollectCars(TrainColor color, int cars)
	{
		CollectedCarsScore.TryGetValue(color, out int score);
		CollectedCarsScore[color] = score + cars;
		OnScoreUpdated?.Invoke();
		CheckVictory();
	}

	public void CheckVictory()
	{
		if (VictoryDeclared)
			return;

		foreach(var score in NeededScore)
		{
			CollectedCarsScore.TryGetValue(score.Key, out int currentlyCollected);
			if (currentlyCollected < score.Value)
				return;
		}

		OnVictory?.Invoke();
		VictoryDeclared = true;
	}

	public void CheckDefeat()
	{

	}

	public void RegisterSpawner(CargoSpawner cargoSpawner)
	{
		AllCargoSpawners.Add(cargoSpawner);
	}

	public Tile GetTile(int x, int y)
	{
		if (x < 0 || x >= LevelData.Width)
			return null;
		if (y < 0 || y >= LevelData.Height)
			return null;

		return tiles[x, y];
	}

	public void Tick(float dT)
	{
		SecondsElapsed += dT;

		foreach (var cargoSpawner in AllCargoSpawners) cargoSpawner.Tick(dT);
		foreach (var train in AllTrains) train.Tick(dT);
	}
}