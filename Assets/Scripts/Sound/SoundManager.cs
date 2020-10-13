using UnityEngine;

public class SoundManager : BaseSoundManager<SoundManager>
{
	[Header("Music")] public AudioClip MusicMainMenu;
	public AudioClip MusicGame;

	[Header("Sound")] public AudioClip SoundTrackSwitch;
	public AudioClip SoundCargoPickUp;
	public AudioClip SoundCargoDropOff;
	public AudioClip SoundTrainStop;
	public AudioClip SoundTrainStart;
	public AudioClip SoundVictory;
	public AudioClip SoundDefeat;

	public void PlayMusicMainMenu()
	{
		PlayMusic(MusicMainMenu);
	}

	public void PlayMusicGame()
	{
		PlayMusic(MusicGame);
	}

	public void PlaySoundTrackSwitch()
	{
		PlaySound(SoundTrackSwitch);
	}

	public void PlaySoundCargoPickUp()
	{
		PlaySound(SoundCargoPickUp);
	}

	public void PlaySoundCargoDropOff()
	{
		PlaySound(SoundCargoDropOff);
	}

	public void PlaySoundTrainStop()
	{
		PlaySound(SoundTrainStop);
	}

	public void PlaySoundTrainStart()
	{
		PlaySound(SoundTrainStart);
	}

	public void PlaySoundVictory()
	{
		PlaySound(SoundVictory);
	}

	public void PlaySoundDefeat()
	{
		PlaySound(SoundDefeat);
	}
}