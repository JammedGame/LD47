using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld
{
    public readonly LevelData LevelData;
    public readonly Tile[,] Tiles;

    readonly List<Train> allTrains = new List<Train>();

    public GameWorld(LevelData levelData)
    {
        this.LevelData = levelData;

        Tiles = new Tile[levelData.Width, levelData.Height];
        for(int i = 0; i < levelData.Width; i++)
            for(int j = 0; j < levelData.Height; j++)
            {
                Tiles[i, j] = new Tile(i, j, levelData.Tiles[i + j * levelData.Width]);
            }

        var train = new Train(this, 1, 0);
        allTrains.Add(train);
    }

    public void Tick()
    {
        foreach(var train in allTrains)
        {
            train.Tick();
        }
    }
}
