using System;
using UnityEngine;

// main controller
public class InGameUIController : MonoBehaviour
{
	[SerializeField] GameObject winnerIsYouControls;

	GameWorld world;
	InGameUIObject[] allUIControls;

	public void Initialize(GameWorld gameWorld)
	{
		this.world = gameWorld;
		this.world.OnVictory += OnVictory;
		this.allUIControls = GameObject.FindObjectsOfType<InGameUIObject>();
		this.winnerIsYouControls.SetActive(false);

		foreach(var obj in allUIControls)
			obj.OnInitialize(world);
	}

	private void OnVictory()
	{
		winnerIsYouControls.SetActive(true);
	}

	public void OnUpdate()
	{
	}
}

public abstract class InGameUIObject : MonoBehaviour
{
	public abstract void OnInitialize(GameWorld gameWorld);
}