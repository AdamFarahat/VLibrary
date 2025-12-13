using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    //Singleton that handles the book interactions and keeps the current information of the previously selected book.
    public static BookManager instance;

    public Image bookDisplayImage;

    public string bookDisplayTitle;

    public string bookText;

    public GameObject EbookCanvas;

    private void Awake()
    {
        //Ensure only one instance of BookManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        //Make sure the image is never visible
        bookDisplayTitle = "";
        bookText = "";
        EbookCanvas.SetActive(false);
        RightPanelManager.onReadButtonPressed.AddListener(OpenEbookCanvas);
    }

    void OpenEbookCanvas()
    {
        EbookCanvas.SetActive(true);
        EbookCanvas.GetComponent<EbookCameraFollow>().PositionInFront();
        EbookCanvas.GetComponent<EbookContent>().UpdateEbookContent();
    }


}
