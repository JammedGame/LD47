using System.Collections.Generic;
using UnityEngine;

public class ViewController
{
	private readonly GameWorld gameWorld;
	private readonly TileView[,] tile2View;
	private readonly TileView tileViewPrefab;
	private readonly List<TrainView> allTrainViews = new List<TrainView>();

	public ViewController(GameWorld gameWorld)
	{
		this.gameWorld = gameWorld;

		tileViewPrefab = Resources.Load<TileView>("Prefabs/TileView");
		tile2View = new TileView[levelData.Width, levelData.Height];
		for (var i = 0; i < levelData.Width; i++)
		for (var j = 0; j < levelData.Height; j++)
			tile2View[i, j] = TileView.CreateView(gameWorld.GetTile(i, j), tileViewPrefab);

		foreach(var train in gameWorld.AllTrains)
			allTrainViews.Add(TrainView.CreateView(train));
	}

	private LevelData levelData => gameWorld.LevelData;

	public void Render()
	{
		foreach(var trainView in allTrainViews)
		{
			trainView.UpdateView();
		}
	}
}

public static class TileViewUtil
{
	public static Vector3 GetPosition3D(this Tile tile)
	{
		return new Vector3(tile.X, -tile.Y, 0);
	}
}