using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontButton : MonoBehaviour
{
    public Button button;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(OnFontButtonPressed);   
    }

    public void OnFontButtonPressed()
    {
        BookManager.Instance.EbookCanvas.GetComponent<EbookContent>().SetTextFont(button.GetComponentInChildren<TextMeshProUGUI>().font);
        Debug.Log("Font Button Pressed: " + button.GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
