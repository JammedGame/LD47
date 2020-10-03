using System;
using UnityEngine;

public class ViewController
{
    private readonly GameWorld gameWorld;
    private readonly TileView[,] tile2View;
    private LevelData levelData => gameWorld.LevelData;

    public ViewController(GameWorld gameWorld)
    {
        this.gameWorld = gameWorld;

        tile2View = new TileView[levelData.Width, levelData.Height];
        for(int i = 0; i < levelData.Width; i++)
            for(int j = 0; j < levelData.Height; j++)
            {
                tile2View[i, j] = CreateView(gameWorld.Tiles[i, j]);
            }        
    }

    private TileView CreateView(Tile tile)
    {
        return null;
    }

    public void Render()
    {
    }
}