using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BookButton : MonoBehaviour
{
    public Image bookImage;

    public string bookTitleText;

    public string bookText;

    public static UnityEvent onBookSelected = new UnityEvent();

    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bookImage = GetComponent<Image>();
        UpdateBookInfo(bookImage.sprite);

        button.onClick.AddListener(OnBookButtonPressed);
    }

    void UpdateBookInfo(Sprite bookSprite)
    {
        if (bookImage != null)
        {
            bookImage.sprite = bookSprite;
        }
    }

    //When the button is pressed, it sends its information to the BookManager
    public void OnBookButtonPressed()
    {
        BookManager.instance.bookDisplayImage.sprite = bookImage.sprite;
        BookManager.instance.bookDisplayTitle = bookTitleText;
        BookManager.instance.bookText = bookText;
        Debug.Log("Book Button Pressed: " + bookTitleText);
        //Let the right panel know a book has been selected
        onBookSelected.Invoke();
    }
}
