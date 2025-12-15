using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SoundButton : MonoBehaviour
{
    public string soundName;

    public AudioClip soundClip;

    public Image soundImage;
    
    [SerializeField] private Button button;

    public static UnityEvent onSoundButtonPressed = new UnityEvent();

    private void Start()
    {
        button.onClick.AddListener(OnSoundButtonClicked);
    }

    private void OnSoundButtonClicked()
    {
        AudioManager.Instance.PrepareNextSound(soundName, soundClip, soundImage);
        onSoundButtonPressed.Invoke();
        Debug.Log("Sound Button Pressed: " + soundName);
    }
}
