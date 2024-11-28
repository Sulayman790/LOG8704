using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public string targetTag; // Tag to match
    public UnityEvent<GameObject> OnEnterEvent = new UnityEvent<GameObject>(); // Initialized
    private int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag)) // Use CompareTag for better performance
        {
            count += 1;
            if (count == 3) // Event triggered only when count equals 3
            {
                if (OnEnterEvent != null)
                {
                    OnEnterEvent.Invoke(other.gameObject);
                }
            }
        }
    }
}