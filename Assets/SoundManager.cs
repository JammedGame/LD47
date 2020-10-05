using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip gameplayAudio;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("1");
        gameplayAudio = Resources.Load<AudioClip>("Audio/swingTheme");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(gameplayAudio);
        }
    }
}
