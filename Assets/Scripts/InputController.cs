using System;
using UnityEngine;

public class InputController
{
	readonly ViewController viewController;
	readonly Camera camera;

	public InputController(ViewController viewController, Camera camera)
	{
		this.camera = camera;
		this.viewController = viewController;
	}

	public void ProcessInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			OnLeftMouseButtonDown();
		}
	}

	private void OnLeftMouseButtonDown()
	{
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		var plane = new Plane(Vector3.forward, Vector3.zero);
		if (plane.Raycast(ray, out float distance))
		{
			var intersectPoint = ray.GetPoint(distance);
			var tileView = viewController.GetTileView(intersectPoint);
			if (tileView != null)
			{
				tileView.Tile.OnClicked();
				tileView.UpdateMaterialAndRotation();
				return;
			}
		}
	}
}