using System;
using UnityEngine;
using UnityEngine.UI;

// main controller
public class InGameUIController : MonoBehaviour
{
	[SerializeField] GameObject winnerIsYouControls = null;
	[SerializeField] GameObject loserIsYouControls = null;
	[SerializeField] GameObject pauseControls = null;
	[SerializeField] Button StartNextLevelButton = null;
	[SerializeField] Button ResumeButton = null;
	[SerializeField] Button BackToMainMenuButton = null;
	[SerializeField] Button BackToMainMenuButton2 = null;
	[SerializeField] Button RestartButton = null;
	[SerializeField] Button RestartButton2 = null;

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

		StartNextLevelButton.onClick.AddListener(() => Game.LoadNextLevel(gameWorld.LevelData));
		RestartButton.onClick.AddListener(() => gameWorld.RestartLevel());
		RestartButton2.onClick.AddListener(() => gameWorld.RestartLevel());
		ResumeButton.onClick.AddListener(() => gameWorld.Resume());
		BackToMainMenuButton.onClick.AddListener(() => Game.BackToMainMenu());
		BackToMainMenuButton2.onClick.AddListener(() => Game.BackToMainMenu());

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