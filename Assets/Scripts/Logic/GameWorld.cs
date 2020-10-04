using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld
{
    public readonly LevelData LevelData;

    readonly Tile[,] tiles;
    readonly List<Train> allTrains = new List<Train>();

    public List<Train> AllTrains => allTrains;

    public GameWorld(LevelData levelData)
    {
        this.LevelData = levelData;

        tiles = new Tile[levelData.Width, levelData.Height];
        for(int i = 0; i < levelData.Width; i++)
            for(int j = 0; j < levelData.Height; j++)
            {
                tiles[i, j] = new Tile(this, i, j, levelData.Tiles[i + j * levelData.Width]);
            }

        foreach(var trainSpawn in levelData.TrainSpawns)
            allTrains.Add(new Train(this, trainSpawn));
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
        foreach(var train in allTrains)
        {
            train.Tick(dT);
        }
    }
}
