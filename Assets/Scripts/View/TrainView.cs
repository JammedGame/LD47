using System;
using UnityEngine;

public class TrainView : MonoBehaviour
{
	private Train train;

	internal static TrainView CreateView(Train train)
	{
		var trainViewPrefab = Resources.Load<TrainView>("Prefabs/TrainView");
		var trainView = GameObject.Instantiate(trainViewPrefab);
		trainView.train = train;
		trainView.UpdateView();
		return trainView;
	}

	public void UpdateView()
	{
		var lastTilePos = train.LastTile.GetPosition3D();
		var newTilePos = train.Tile.GetPosition3D();
		transform.position = Vector3.Lerp(lastTilePos, newTilePos, train.ProgressToTile);
	}
}