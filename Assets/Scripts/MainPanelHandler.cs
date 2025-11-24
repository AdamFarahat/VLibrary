using UnityEngine;

public class MainPanelHandler : MonoBehaviour
{
    [SerializeField] GameObject LibraryPanel;

    [SerializeField] GameObject SoundsPanel;

    [SerializeField] GameObject EnvironmentPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ButtonEventHandler.Instance.onLibraryButtonClicked.AddListener(() =>
        {
            Debug.Log("Library Button Clicked - handled in MainPanelHandler");
            LibraryPanel.SetActive(true);
            SoundsPanel.SetActive(false);
            EnvironmentPanel.SetActive(false);
        });
        ButtonEventHandler.Instance.onSoundsButtonClicked.AddListener(() =>
        {
            Debug.Log("Sounds Button Clicked - handled in MainPanelHandler");
            SoundsPanel.SetActive(true);
            LibraryPanel.SetActive(false);
            EnvironmentPanel.SetActive(false);
        });
        ButtonEventHandler.Instance.onEnvironmentButtonClicked.AddListener(() =>
        {
            Debug.Log("Environment Button Clicked - handled in MainPanelHandler");
            EnvironmentPanel.SetActive(true);
            LibraryPanel.SetActive(false);
            SoundsPanel.SetActive(false);
        });

        SoundsPanel.SetActive(false);
        EnvironmentPanel.SetActive(false);
        LibraryPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
