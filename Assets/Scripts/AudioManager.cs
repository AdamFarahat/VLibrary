using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public string nextSoundName;

    public AudioClip nextSoundClip;

    public AudioSource audioSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public void PrepareNextSound(string soundName, AudioClip soundClip)
    {
        nextSoundName = soundName;
        nextSoundClip = soundClip;
        // Additional logic to handle the sound can be added here
    }

    public void PlayNextSound()
    {
        if (nextSoundClip != null && audioSource != null)
        {
            audioSource.clip = nextSoundClip;
            audioSource.Play();
            Debug.Log("Playing Sound: " + nextSoundName);
        }
        else
        {
            Debug.LogWarning("No sound prepared to play.");
        }
    }
}
