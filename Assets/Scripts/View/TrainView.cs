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
		var tileEnterPos = train.Tile.GetPosition3D(train.TileEnterDirection);
		var tileExitPos = train.Tile.GetPosition3D(train.Direction);
		transform.position = Vector3.Lerp(tileEnterPos, tileExitPos, train.ProgressInsideTile);
	}
}