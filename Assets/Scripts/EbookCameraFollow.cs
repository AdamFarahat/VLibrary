// ...existing code...
using UnityEngine;

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
    // If another system is reparenting this object after Start, enable this
    public bool forceUnparentEveryFrame = false;

    // Helpful debug info
    public bool debugLogs = false;

    void Awake()
{
    ebookCanvas = GetComponent<Canvas>();
    if (ebookCanvas == null)
    {
        Debug.LogWarning("EbookCameraFollow: No Canvas component found on this GameObject.");
    }
    else if (forceWorldSpace)
    {
        if (ebookCanvas.renderMode != RenderMode.WorldSpace)
        {
            
            // ensure no negative scale
            ebookCanvas.transform.localScale = Vector3.one;
        }
        if (Camera.main != null)
            ebookCanvas.worldCamera = Camera.main;
    }

    ebookCanvas.renderMode = RenderMode.WorldSpace;
    // don't flip the canvas — keep default rotation so text isn't reversed
    ebookCanvas.transform.localRotation = Quaternion.identity;

    // Unparent early so local parenting won't override position
    transform.SetParent(null);
}

    void Start()
    {
        // Try main camera first, otherwise pick the first enabled camera
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
    }

    void LateUpdate()
    {
        if (cameraTransform == null) return;

        if (forceUnparentEveryFrame)
            transform.SetParent(null);

        // Ensure root and canvas scales are positive to avoid mirrored UI
        Vector3 rootScale = transform.localScale;
        transform.localScale = new Vector3(Mathf.Abs(rootScale.x), Mathf.Abs(rootScale.y), Mathf.Abs(rootScale.z));
        Vector3 canvasScale = ebookCanvas != null ? ebookCanvas.transform.localScale : Vector3.one;
        if (ebookCanvas != null)
            ebookCanvas.transform.localScale = new Vector3(Mathf.Abs(canvasScale.x), Mathf.Abs(canvasScale.y), Mathf.Abs(canvasScale.z));

        Vector3 targetPos = cameraTransform.position + cameraTransform.forward * distance + cameraTransform.up * verticalOffset;
        transform.position = targetPos;

        if (faceCamera)
        {
            // Face the camera while keeping canvas upright using camera's up vector
            transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position, cameraTransform.up);
        }
        else
        {
            transform.rotation = cameraTransform.rotation;
        }

        if (debugLogs)
        {
            float actualDistance = Vector3.Distance(cameraTransform.position, transform.position);
            Debug.Log($"EbookCameraFollow: targetDistance={distance:F2}, actualDistance={actualDistance:F2}, pos={transform.position}");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (cameraTransform == null || !Application.isPlaying) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(cameraTransform.position + cameraTransform.forward * distance + cameraTransform.up * verticalOffset, 0.02f);
    }
}
