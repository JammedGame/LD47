using System;
using UnityEngine;

public class ViewController
{
    private readonly GameWorld gameWorld;
    private readonly TileView[,] tile2View;
    private readonly TileView tileViewPrefab;
    private LevelData levelData => gameWorld.LevelData;

    public ViewController(GameWorld gameWorld)
    {
        this.gameWorld = gameWorld;
        this.tileViewPrefab = Resources.Load<TileView>("Prefabs/TileView");

        tile2View = new TileView[levelData.Width, levelData.Height];
        for(int i = 0; i < levelData.Width; i++)
            for(int j = 0; j < levelData.Height; j++)
            {
                tile2View[i, j] = TileView.CreateView(gameWorld.Tiles[i, j], tileViewPrefab);
            }        
    }

    public void Render()
    {
    }
}

public static class TileViewUtil
{
    public static Vector3 GetPosition3D(this Tile tile)
    {
        return new Vector3(tile.X, tile.Y, 0);
    }
}