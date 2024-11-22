using UnityEngine;
using UnityEngine.Events;

public class SphereVisibilityDetector : MonoBehaviour
{
    private Camera mainCamera;
    public float focusZoneSize = 0.2f;
    private bool isVisible = false;
    public UnityEvent StarFound;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Method 1: Check if object is within camera's view frustum
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        bool inFocusZone = IsInFocusZone(viewportPoint);

        // Method 2: Check if there are no obstacles between camera and sphere
        if (inFocusZone)
        {
            Vector3 directionToCamera = mainCamera.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, -directionToCamera.normalized, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if (!isVisible)
                    {
                        isVisible = true;
                        StarFound.Invoke();
                    }
                }
            }
        }
    }
    private bool IsInFocusZone(Vector3 viewportPoint)
    {
        // Ensure the object is within the screen bounds and near the center
        float centerX = 0.5f; // Middle of the screen in the X-axis
        float centerY = 0.5f; // Middle of the screen in the Y-axis
        float halfFocusZone = focusZoneSize / 2f;

        return viewportPoint.x >= (centerX - halfFocusZone) &&
               viewportPoint.x <= (centerX + halfFocusZone) &&
               viewportPoint.y >= (centerY - halfFocusZone) &&
               viewportPoint.y <= (centerY + halfFocusZone) &&
               viewportPoint.z > 0; // Object must still be in front of the camera
    }
}
