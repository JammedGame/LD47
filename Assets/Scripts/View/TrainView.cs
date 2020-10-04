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
		trainView.Update();
		return trainView;
	}

	public void Update()
	{
		transform.position = train.Tile.GetPosition3D();
	}
}