using System;
using UnityEngine;

public class TrainView : MonoBehaviour
{
	[SerializeField] private Transform[] vagoni;
	[SerializeField] private Transform lokomotiva;
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
		UpdateTransform(lokomotiva, train.GetSnapshot());

		for (int i = 0; i < vagoni.Length; i++)
		{
			UpdateTransform(vagoni[i], train.GetSnapshotFromHistory((i + 1) * vagoniTestOffset));
		}
	}

	public static void UpdateTransform(Transform transform, PositionState state)
	{
		if (state.Tile == null)
			throw new NullReferenceException();

		var tileEnterPos = state.Tile.GetPosition3D(state.EnterDirection);
		var tileExitPos = state.Tile.GetPosition3D(state.ExitDirection);

		if (state.EnterDirection == state.ExitDirection.Opposite())
		{
			transform.position = Vector3.Lerp(tileEnterPos, tileExitPos, state.ProgressInTile);
		}
		else
		{
			var cornerPos = state.Tile.GetCorner(state.EnterDirection, state.ExitDirection);
			var enterPosDir = tileEnterPos - cornerPos;
			var exitPosDir = tileExitPos - cornerPos;
			transform.position = cornerPos + Vector3.Slerp(enterPosDir, exitPosDir, state.ProgressInTile).normalized * 0.5f;
		}

		var enterRotation = Quaternion.Euler(0, 0, state.EnterDirection.Opposite().ToAngle());
		var exitRotation = Quaternion.Euler(0, 0, state.ExitDirection.ToAngle());
		transform.rotation = Quaternion.Slerp(enterRotation, exitRotation, state.ProgressInTile);
	}
}