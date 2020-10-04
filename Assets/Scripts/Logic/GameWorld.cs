using System.Collections.Generic;

public class GameWorld
{
	private readonly List<CargoSpawn> cargoSpawnsRemaining = new List<CargoSpawn>();

	private readonly List<Tile> tilesToUpdate = new List<Tile>();
	public readonly LevelData LevelData;

	private readonly Tile[,] tiles;

	public GameWorld(LevelData levelData)
	{
		SecondsElapsed = 0f;

		SoundManager.PlaySound();

		LevelData = levelData;

		tiles = new Tile[levelData.Width, levelData.Height];
		for (var i = 0; i < levelData.Width; i++)
		for (var j = 0; j < levelData.Height; j++)
		{
			var tileType = levelData.Tiles[i + j * levelData.Width];
			var tile = new Tile(this, i, j, tileType);
			tiles[i, j] = tile;
			if (tileType != TileType.Undefined) tilesToUpdate.Add(tile);
		}

		foreach (var trainSpawn in levelData.TrainSpawns)
			AllTrains.Add(new Train(this, trainSpawn));

		cargoSpawnsRemaining.AddRange(levelData.CargoSpawns);
	}

	public float SecondsElapsed { get; private set; }

	public List<Train> AllTrains { get; } = new List<Train>();

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

		for (var i = cargoSpawnsRemaining.Count - 1; i >= 0; i--)
		{
			var cargoSpawn = cargoSpawnsRemaining[i];
			if (SecondsElapsed >= cargoSpawn.SpawnsAtSeconds)
			{
				var tile = GetTile(cargoSpawn.X, cargoSpawn.Y);
				tile.Cargoes.Add(new Cargo(cargoSpawn.Color, cargoSpawn.DespawnsAfterSeconds));
				cargoSpawnsRemaining.RemoveAt(i);
			}
		}

		foreach (var train in AllTrains) train.Tick(dT);

		foreach (var tile in tilesToUpdate) tile.Tick(dT);
	}
}