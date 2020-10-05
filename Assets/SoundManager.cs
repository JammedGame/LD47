using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager instance;

    public void OnEnable()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    public void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
