using System.Collections.Generic;
using UnityEngine;

public class ViewController
{
	private readonly List<TrainView> allTrainViews = new List<TrainView>();
	private readonly GameWorld gameWorld;
	private readonly TileView[,] tile2View;
	private readonly List<TileView> tilesToUpdate = new List<TileView>();
	private readonly TileView tileViewPrefab;

	public ViewController(GameWorld gameWorld)
	{
		this.gameWorld = gameWorld;

		tileViewPrefab = Resources.Load<TileView>("Prefabs/TileView");
		tile2View = new TileView[levelData.Width, levelData.Height];
		for (var i = 0; i < levelData.Width; i++)
		for (var j = 0; j < levelData.Height; j++)
		{
			var tile = gameWorld.GetTile(i, j);
			var tileView = TileView.CreateView(tile, tileViewPrefab);
			tile2View[i, j] = tileView;
			if (tile.TileType != TileType.Undefined) tilesToUpdate.Add(tileView);
		}

		foreach (var train in gameWorld.AllTrains)
			allTrainViews.Add(TrainView.CreateView(train));
	}

	private LevelData levelData => gameWorld.LevelData;

	public void Render()
	{
		foreach (var trainView in allTrainViews) trainView.UpdateView();

		foreach (var tileView in tilesToUpdate) tileView.UpdateView();
	}

	public TileView GetTileView(Vector3 pos)
	{
		var x = Mathf.RoundToInt(pos.x);
		var y = Mathf.RoundToInt(-pos.y);
		if (x < 0 || x >= levelData.Width) return null;
		if (y < 0 || y >= levelData.Height) return null;
		return tile2View[x, y];
	}
}

public static class TileViewUtil
{
	public static Vector3 GetPosition3D(this Tile tile)
	{
		return new Vector3(tile.X, -tile.Y, 0);
	}

	public static Vector3 GetPosition3D(this Tile tile, Direction direction)
	{
		switch (direction)
		{
			case Direction.Left: return new Vector3(tile.X - 0.5f, -tile.Y, 0);
			case Direction.Right: return new Vector3(tile.X + 0.5f, -tile.Y, 0);
			case Direction.Top: return new Vector3(tile.X, -tile.Y + 0.5f, 0);
			case Direction.Bottom: return new Vector3(tile.X, -tile.Y - 0.5f, 0);
			default:
				return new Vector3(tile.X, -tile.Y, 0);
		}
	}
}