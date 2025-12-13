using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class RightPanelManager : MonoBehaviour
{
    //Handles showing the user the information of any button they selected from the center panel.

    TextMeshProUGUI titleText;

    public Image image;

    public Button startButton;

    public enum PanelState
    {
        Default,
        BookSelected
    }

    private PanelState currentState = PanelState.Default;

    public static UnityEvent onReadButtonPressed = new UnityEvent();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get references to the UI components
        titleText = GetComponentInChildren<TextMeshProUGUI>();

        //Subscribe to the BookButton's event to update the right panel when a book is selected
        BookButton.onBookSelected.AddListener(UpdateRightPanelForBook);

        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    void UpdateRightPanelForBook()
    {
        Debug.Log("Updating Right Panel with selected book info.");
        //Update the right panel with the selected book's information
        titleText.text = BookManager.instance.bookDisplayTitle;
        image.sprite = BookManager.instance.bookDisplayImage.sprite;

        //Enable the start button when a book is selected
        startButton.interactable = true;
        currentState = PanelState.BookSelected;
    }

    public void OnStartButtonPressed()
    {
        if (currentState == PanelState.BookSelected)
        {
            //Open the ebook canvas to read the book

            onReadButtonPressed.Invoke();
            Debug.Log("Start Button Pressed: Opening Ebook Canvas for " + BookManager.instance.bookDisplayTitle);
        }
    }


}
