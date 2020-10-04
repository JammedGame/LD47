using System;
using UnityEngine;

public class TrainView : MonoBehaviour
{
	[SerializeField] private Transform[] vagoni;
	[SerializeField] private Transform lokomotiva;
	[SerializeField] private int vagoniTestOffset = 10;
	[SerializeField] private MeshRenderer iconRenderer;
	private int carsVisible;

	private Train train;

	internal static TrainView CreateView(Train train)
	{
		var trainViewPrefab = Resources.Load<TrainView>("Prefabs/TrainView");
		var trainView = Instantiate(trainViewPrefab);
		trainView.train = train;
		// todo jole
		// trainView.iconRenderer.material.mainTexture = train.Type.LoadLocomotiveTexture();
		trainView.carsVisible = -1;
		trainView.UpdateView();
		return trainView;
	}

	public void UpdateView()
	{
		UpdateTransform(lokomotiva, train.GetSnapshot());

		for (var i = 0; i < vagoni.Length; i++)
			UpdateTransform(vagoni[i], train.GetSnapshotFromHistory((i + 1) * vagoniTestOffset));

		if (train.Cars != carsVisible)
		{
			for (var i = 0; i < vagoni.Length; i++)
			{
				var car = vagoni[i];
				car.gameObject.SetActive(i < train.Cars);
			}

			carsVisible = train.Cars;
		}
	}

	public static void UpdateTransform(Transform transform, PositionState state)
	{
		if (state.Tile == null)
			throw new NullReferenceException();

		transform.position = state.GetPosition();

		var enterRotation = Quaternion.Euler(0, 0, state.EnterDirection.Opposite().ToAngle());
		var exitRotation = Quaternion.Euler(0, 0, state.ExitDirection.ToAngle());
		transform.rotation = Quaternion.Slerp(enterRotation, exitRotation, state.ProgressInTile);
	}
}