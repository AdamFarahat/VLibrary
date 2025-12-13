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
            ebookText.text = BookManager.instance.bookText;
            Debug.Log("Ebook content updated.");
        }
        else
        {
            Debug.LogWarning("EbookContent: No TextMeshProUGUI component assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
