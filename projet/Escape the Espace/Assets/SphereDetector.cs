using UnityEngine;

public class SphereVisibilityDetector : MonoBehaviour
{
    public GameObject fryingPan;
    private Camera mainCamera;
    private Renderer sphereRenderer;
    private bool isVisible = false;

    void Start()
    {
        mainCamera = Camera.main;
        sphereRenderer = GetComponent<Renderer>();

        // Make sure fryingPan starts invisible
        if (fryingPan != null)
        {
            fryingPan.SetActive(false);
        }
    }

    void Update()
    {
        CheckVisibility();
    }

    void CheckVisibility()
    {
        // Method 1: Check if object is within camera's view frustum
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool inCameraView = viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                           viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                           viewportPoint.z > 0;

        // Method 2: Check if there are no obstacles between camera and sphere
        if (inCameraView)
        {
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToCamera, out hit))
            {
                if (hit.collider.CompareTag("MainCamera"))
                {
                    if (!isVisible)
                    {
                        OnBecameVisible();
                    }
                    isVisible = true;
                    return;
                }
            }
        }

        if (isVisible)
        {
            OnBecameInvisible();
        }
        isVisible = false;
    }

    // Rest of the code stays the same
    void OnBecameVisible()
    {
        if (fryingPan != null)
        {
            fryingPan.SetActive(true);
            Debug.Log("Sphere is visible and fryingPan should appear");
        }
    }

    void OnBecameInvisible()
    {
        Debug.Log("Sphere is no longer visible to player");
    }

}
