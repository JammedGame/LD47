using UnityEngine;

public class MainMenu : MonoBehaviour
{
	private void Start()
	{
		SoundManager.Instance.PlayMusicMainMenu();
	}

	public void PlayGame()
	{
		Game.LoadLevel(0);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}