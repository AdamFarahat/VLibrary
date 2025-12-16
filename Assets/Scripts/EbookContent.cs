using UnityEngine;
using TMPro;

public class EbookContent : MonoBehaviour
{
    public TextMeshProUGUI ebookText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RightPanelManager.onReadButtonPressed.AddListener(UpdateEbookContent);
    }

    public void UpdateEbookContent()
    {
        if (ebookText != null)
        {
            ebookText.text = BookManager.Instance.bookText;
            Debug.Log("Ebook content updated.");
        }
        else
        {
            Debug.LogWarning("EbookContent: No TextMeshProUGUI component assigned.");
        }
    }

    public void SetTextSize(int size)
    {
        if (ebookText != null)
        {
            ebookText.fontSize = size;
            Debug.Log("Ebook text size set to: " + size);
        }
        else
        {
            Debug.LogWarning("EbookContent: No TextMeshProUGUI component assigned.");
        }
    }

    public void SetTextFont(TMP_FontAsset font)
    {
        if (ebookText != null)
        {
            ebookText.font = font;
            Debug.Log("Ebook text font set to: " + font.name);
        }
        else
        {
            Debug.LogWarning("EbookContent: No TextMeshProUGUI component assigned.");
        }
    }
}
