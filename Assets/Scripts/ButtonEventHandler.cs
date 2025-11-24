using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonEventHandler : MonoBehaviour
{
    public static ButtonEventHandler Instance { get; private set; }
    public UnityEvent onLibraryButtonClicked = new UnityEvent();
    public UnityEvent onSoundsButtonClicked = new UnityEvent();
    public UnityEvent onEnvironmentButtonClicked = new UnityEvent();

    [SerializeField] private Button libraryButton;

    [SerializeField] private Button soundsButton;

    [SerializeField] private Button environmentButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        libraryButton.onClick.AddListener(() => onLibraryButtonClicked.Invoke());
        soundsButton.onClick.AddListener(() => onSoundsButtonClicked.Invoke());
        environmentButton.onClick.AddListener(() => onEnvironmentButtonClicked.Invoke());
    }
}
