using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider textSizeSlider;

    public Slider volumeSlider;
    
    private void Start()
    {
        volumeSlider.value = AudioManager.Instance.audioSource.volume;

        volumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.audioSource.volume = value;
            Debug.Log("Volume set to: " + value);
        });

        // The text size will be from 18 to 48
        textSizeSlider.minValue = 18;
        textSizeSlider.maxValue = 48;
        textSizeSlider.value = 18;

        textSizeSlider.onValueChanged.AddListener((value) =>
        {
            BookManager.Instance.EbookCanvas.GetComponent<EbookContent>().SetTextSize((int)value);
        });
    }
}
