using TMPro;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
	public TextMeshProUGUI ToggleMusicLabel;
	public TextMeshProUGUI ToggleSoundLabel;

	private void Start()
	{
		UpdateLabels();
	}

	public void ToggleMusic()
	{
		SoundManager.Instance.MusicEnabled = !SoundManager.Instance.MusicEnabled;
		UpdateLabels();
	}

	public void ToggleSound()
	{
		SoundManager.Instance.SoundEnabled = !SoundManager.Instance.SoundEnabled;
		UpdateLabels();
	}

	private void UpdateLabels()
	{
		ToggleMusicLabel.text = SoundManager.Instance.MusicEnabled ? "MUTE MUSIC" : "UNMUTE MUSIC";
		ToggleSoundLabel.text = SoundManager.Instance.SoundEnabled ? "MUTE SOUND" : "UNMUTE SOUND";
	}
}