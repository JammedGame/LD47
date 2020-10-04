using System;
using UnityEngine;

public class TrainView : MonoBehaviour
{
	[SerializeField] private Transform[] vagoni;
	[SerializeField] private int vagoniTestOffset = 10;

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
		UpdateTransform(transform, train.GetSnapshot());

		for (int i = 0; i < vagoni.Length; i++)
		{
			UpdateTransform(vagoni[i], train.GetSnapshotFromHistory((i + 1) * vagoniTestOffset));
		}
	}

	public void UpdateTransform(Transform transform, PositionState state)
	{
		if (state.Tile == null)
			throw new NullReferenceException();

		var tileEnterPos = state.Tile.GetPosition3D(state.EnterDirection);
		var tileExitPos = state.Tile.GetPosition3D(state.ExitDirection);
		transform.position = Vector3.Lerp(tileEnterPos, tileExitPos, state.ProgressInTile);
	}
}