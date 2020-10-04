using System;
using System.Collections.Generic;
using UnityEngine;

public class TrainView : MonoBehaviour
{
	[SerializeField] private GameObject carPrefab;
	[SerializeField] private Transform lokomotiva;
	[SerializeField] private int vagoniTestOffset = 10;
	[SerializeField] private MeshRenderer iconRenderer;
	[SerializeField] private float carOffsetY = -0.9f;

	private Train train;
	private readonly List<TrainView> vagoni = new List<TrainView>();
	private int carsVisible;

	internal static TrainView CreateView(Train train)
	{
		var trainViewPrefab = Resources.Load<TrainView>("Prefabs/TrainView");
		var trainView = Instantiate(trainViewPrefab);
		trainView.train = train;
		trainView.iconRenderer.material.mainTexture = train.Color.LoadLocomotiveTexture();
		trainView.carsVisible = -1;
		trainView.UpdateView();
		return trainView;
	}

	public void UpdateView()
	{
		UpdateTransform(lokomotiva, train.GetSnapshot());

		while (train.Cars > vagoni.Count)
		{
			var newCarObject = Instantiate(carPrefab, transform);
			vagoni.Add(newCarObject.GetComponent<TrainView>());
			newCarObject.transform.Translate(0f, vagoni.Count * carOffsetY, 0f);
		}

		if (train.Cars != carsVisible)
		{
			for (var i = 0; i < vagoni.Count; i++)
			{
				var car = vagoni[i];
				if (i < train.Cars)
				{
					car.gameObject.SetActive(true);
					car.iconRenderer.material.mainTexture = train.Color.LoadCarTexture();
				}
				else
				{
					car.gameObject.SetActive(false);
				}
			}

			carsVisible = train.Cars;
		}

		for (var i = 0; i < vagoni.Count; i++)
			UpdateTransform(vagoni[i].transform, train.GetSnapshotFromHistory((i + 1) * vagoniTestOffset));
	}

	public static void UpdateTransform(Transform transform, PositionState state)
	{
		if (state.Tile == null)
			throw new NullReferenceException();

		transform.position = state.GetPosition();
		transform.rotation = Quaternion.Euler(0, 0, state.GetAngle());
	}
}