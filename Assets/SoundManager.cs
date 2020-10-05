using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource MusicSource;
	public AudioSource SoundSource;

	[Header("Music")] public AudioClip MusicMainMenu;
	public AudioClip MusicGame;

	[Header("Sound")] public AudioClip SoundTrackSwitch;
	public AudioClip SoundCargoSpawn;
	public AudioClip SoundCargoPickUp;
	public AudioClip SoundTrainStop;
	public AudioClip SoundTrainStart;
	public AudioClip SoundVictory;
	public AudioClip SoundGameOver;

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

	public void PlayMusicMainMenu()
	{
		MusicSource.Stop();
		MusicSource.clip = MusicMainMenu;
		MusicSource.Play();
	}

	public void PlayMusicGame()
	{
		MusicSource.Stop();
		MusicSource.clip = MusicGame;
		MusicSource.Play();
	}
}