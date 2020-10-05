using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
	public Image LevelPreviewImage;

	private int levelIndex;

	public int LevelIndex
	{
		get => levelIndex;
		set
		{
			levelIndex = value;
			LevelPreviewImage.sprite = Resources.Load<Sprite>($"Art/LevelSelection/LevelSelection{value + 1:00}");
		}
	}

	private void OnEnable()
	{
		LevelIndex = 0;
	}

	public void Previous()
	{
		if (LevelIndex > 0) LevelIndex--;
	}

	public void Next()
	{
		if (LevelIndex < GameSettings.Instance.AllLevels.Count - 1) LevelIndex++;
	}

	public void PlayLevel()
	{
		Game.LoadLevel(LevelIndex);
	}
}