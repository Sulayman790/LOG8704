using UnityEngine;
using UnityEngine.Events;

public class TriggerTrash : MonoBehaviour
{
    public string targetTag; // Tag to match
    public UnityEvent<GameObject> OnEnterEvent = new UnityEvent<GameObject>(); // Initialized

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(targetTag)) // Use CompareTag for better performance
        {
            OnEnterEvent.Invoke(other.gameObject);
        }
    }
}