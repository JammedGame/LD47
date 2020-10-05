using System;
using UnityEngine;

// main controller
public class InGameUIController : MonoBehaviour
{
	[SerializeField] GameObject winnerIsYouControls;
	[SerializeField] GameObject loserIsYouControls;
	[SerializeField] GameObject pauseControls;

	GameWorld world;
	InGameUIObject[] allUIControls;

	public void Initialize(GameWorld gameWorld)
	{
		this.world = gameWorld;
		this.allUIControls = GameObject.FindObjectsOfType<InGameUIObject>();

		this.winnerIsYouControls.SetActive(false);
		this.loserIsYouControls.SetActive(false);
		this.pauseControls.SetActive(false);

		this.world.OnVictory += OnVictory;
		this.world.OnDefeat += OnDefeat;
		this.world.OnPause += OnPaused;

		foreach(var obj in allUIControls)
			obj.OnInitialize(world);
	}

	private void OnDefeat()
	{
		loserIsYouControls.SetActive(true);
	}

	private void OnVictory()
	{
		winnerIsYouControls.SetActive(true);
	}

	private void OnPaused(bool isPaused)
	{
		pauseControls.SetActive(isPaused);
	}

	public void OnUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
		{
			world.TogglePause();
		}
	}
}

public abstract class InGameUIObject : MonoBehaviour
{
	public abstract void OnInitialize(GameWorld gameWorld);
}