using UnityEngine;

public class SphereActivator : MonoBehaviour
{
    public GameObject fryingPan;
    private bool isActivated = false;

    void Start()
    {
        // Make sure fryingPan is hidden at start
        if (fryingPan != null)
        {
            fryingPan.SetActive(false);
        }
    }

    // This method can be called when your activation condition is met
    public void ActivateSphere()
    {
        if (!isActivated && fryingPan != null)
        {
            fryingPan.SetActive(true);
            isActivated = true;
        }
    }

    // Optional: Method to deactivate if needed
    public void DeactivateSphere()
    {
        if (isActivated && fryingPan != null)
        {
            fryingPan.SetActive(false);
            isActivated = false;
        }
    }

    void Update()
    {
        // Simple visibility check using raycast
        Vector3 directionToCamera = Camera.main.transform.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, directionToCamera, out hit))
        {
            if (hit.collider.CompareTag("MainCamera"))
            {
                ActivateSphere();
            }
        }
    }

}
