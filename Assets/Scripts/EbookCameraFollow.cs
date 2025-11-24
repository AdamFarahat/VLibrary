using UnityEngine;
using UnityEngine.XR;

public class EbookCameraFollow : MonoBehaviour
{
    Canvas ebookCanvas;
    Transform cameraTransform;

    // distance in front of the camera
    public float distance = 2f;
    // vertical offset relative to camera forward
    public float verticalOffset = 0f;
    // if true, make the canvas face the camera
    public bool faceCamera = true;

    // If true, force the Canvas to World Space in Awake
    public bool forceWorldSpace = true;

    // Helpful debug info
    public bool debugLogs = false;

    // Input state
    XRNode inputNode = XRNode.RightHand;
    bool prevPrimary = false;

    void Awake()
    {
        ebookCanvas = GetComponent<Canvas>();
        if (ebookCanvas == null)
        {
            Debug.LogWarning("EbookCameraFollow: No Canvas component found on this GameObject.");
        }
        else
        {
            if (forceWorldSpace)
            {
                ebookCanvas.renderMode = RenderMode.WorldSpace;
                if (Camera.main != null)
                    ebookCanvas.worldCamera = Camera.main;
            }

            // ensure positive scale and default rotation
            ebookCanvas.transform.localScale = new Vector3(Mathf.Abs(ebookCanvas.transform.localScale.x),
                                                           Mathf.Abs(ebookCanvas.transform.localScale.y),
                                                           Mathf.Abs(ebookCanvas.transform.localScale.z));
            ebookCanvas.transform.localRotation = Quaternion.identity;
        }

        // keep the object unparented so camera movement doesn't automatically move it
        transform.SetParent(null);
    }

    void Start()
    {
        // find camera
        Camera cam = Camera.main;
        if (cam == null)
        {
            var cams = Camera.allCameras;
            for (int i = 0; i < cams.Length; i++)
            {
                if (cams[i] != null && cams[i].enabled)
                {
                    cam = cams[i];
                    break;
                }
            }
        }

        if (cam != null)
            cameraTransform = cam.transform;
        else
            Debug.LogWarning("EbookCameraFollow: No camera found. Tag your camera as MainCamera or assign one.");

        // Position once at start
        PositionInFront();
    }

    void Update()
    {
        // Read primary button (A on right Quest) and trigger a reposition on rising edge
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputNode);
        bool primary = false;
        if (device.isValid)
        {
            device.TryGetFeatureValue(CommonUsages.primaryButton, out primary);
        }

        if (primary && !prevPrimary)
        {
            // A pressed -> reposition in front of camera
            PositionInFront();
            if (debugLogs) Debug.Log("EbookCameraFollow: A pressed - repositioned canvas.");
        }

        prevPrimary = primary;
    }

    void PositionInFront()
    {
        if (cameraTransform == null) return;

        // ensure positive scales to avoid mirrored text
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
        if (ebookCanvas != null)
            ebookCanvas.transform.localScale = new Vector3(Mathf.Abs(ebookCanvas.transform.localScale.x), Mathf.Abs(ebookCanvas.transform.localScale.y), Mathf.Abs(ebookCanvas.transform.localScale.z));

        // place in front of the camera
        Vector3 targetPos = cameraTransform.position + cameraTransform.forward * distance + cameraTransform.up * verticalOffset;
        transform.position = targetPos;

        if (faceCamera)
        {
            // Look at camera then flip 180 so the visible face is correct
            transform.LookAt(cameraTransform.position, cameraTransform.up);
            transform.Rotate(0f, 180f, 0f, Space.Self);
        }
        else
        {
            transform.rotation = cameraTransform.rotation;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (cameraTransform == null || !Application.isPlaying) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(cameraTransform.position + cameraTransform.forward * distance + cameraTransform.up * verticalOffset, 0.02f);
    }
}
