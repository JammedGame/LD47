using UnityEngine;

public class BaseSoundManager<T> : MonoBehaviour where T : BaseSoundManager<T>
{
	private static BaseSoundManager<T> _instance;

	public AudioSource musicSource;
	public AudioSource soundSource;

	public static T Instance => (T) _instance;

	public bool SoundEnabled
	{
		get => !soundSource.mute;
		set => soundSource.mute = !value;
	}

	public bool MusicEnabled
	{
		get => !musicSource.mute;
		set => musicSource.mute = !value;
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (_instance != this)
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
		if (_instance == this) _instance = null;
	}

	protected void PlayMusic(AudioClip clip)
	{
		if (musicSource.clip == clip && musicSource.isPlaying) return;

		musicSource.Stop();
		if (clip == null) return;

		musicSource.loop = true;
		musicSource.clip = clip;
		musicSource.Play();
	}

	protected void PlaySound(AudioClip clip)
	{
		if (soundSource.isPlaying) return;

		soundSource.Stop();
		if (clip == null) return;

		soundSource.loop = false;
		soundSource.clip = clip;
		soundSource.Play();
	}
}