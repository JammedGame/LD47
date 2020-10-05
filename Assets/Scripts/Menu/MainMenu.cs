using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public GameObject QuitButton;

	private void Start()
	{
		SoundManager.Instance.PlayMusicMainMenu();
#if UNITY_EDITOR || !UNITY_WEBGL
		QuitButton.SetActive(true);
#else
		QuitButton.SetActive(false);
#endif
	}

	public void PlayGame()
	{
		Game.LoadLevel(0);
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#elif !UNITY_WEBGL
		Application.Quit();
#endif
	}
}