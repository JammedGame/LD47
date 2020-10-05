using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
	public TextMeshProUGUI LevelName;
	public Image LevelPreviewImage;

	private int levelIndex;

	public int LevelIndex
	{
		get => levelIndex;
		set
		{
			levelIndex = value;
			LevelName.text = $"Level {value + 1}";
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
		if (LevelIndex < GameSettings.Instance.AllLevels.Count - 2) LevelIndex++;
	}

	public void PlayLevel()
	{
		Game.LoadLevel(LevelIndex);
	}
}