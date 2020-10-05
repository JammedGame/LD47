using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource MusicSource;
	public AudioSource SoundSource;

	[Header("Music")] public AudioClip MusicMainMenu;
	public AudioClip MusicGame;

	[Header("Sound")] public AudioClip SoundTrackSwitch;
	public AudioClip SoundCargoPickUp;
	public AudioClip SoundCargoDropOff;
	public AudioClip SoundTrainStop;
	public AudioClip SoundTrainStart;
	public AudioClip SoundVictory;
	public AudioClip SoundDefeat;

	public static SoundManager Instance { get; private set; }

	public bool SoundEnabled
	{
		get => !SoundSource.mute;
		set => SoundSource.mute = !value;
	}

	public bool MusicEnabled
	{
		get => !MusicSource.mute;
		set => MusicSource.mute = !value;
	}

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		SoundEnabled = true;
		MusicEnabled = true;
	}

	private void OnDestroy()
	{
		if (Instance == this) Instance = null;
	}

	private void PlayMusic(AudioClip clip)
	{
		if (MusicSource.clip == clip && MusicSource.isPlaying) return;

		MusicSource.Stop();
		MusicSource.loop = true;
		MusicSource.clip = clip;
		MusicSource.Play();
	}

	private void PlaySound(AudioClip clip)
	{
		if (SoundSource.isPlaying) return;

		SoundSource.Stop();
		SoundSource.loop = false;
		SoundSource.clip = clip;
		SoundSource.Play();
	}

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