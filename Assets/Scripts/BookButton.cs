using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BookButton : MonoBehaviour
{
    Image bookImage;

    TextMeshProUGUI bookTitleText;

    UEPub.UEPubReader epubFile;

    public UnityEvent onBookSelected = new UnityEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bookImage = GetComponent<Image>();
        bookTitleText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateBookInfo(bookImage.sprite);
    }

    void UpdateBookInfo(Sprite bookSprite)
    {
        if (bookImage != null)
        {
            bookImage.sprite = bookSprite;
        }
    }
}
