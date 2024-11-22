using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public string targetTag;
    public UnityEvent<GameObject> OnEnterEvent;
    private int count = 0;

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == targetTag){
            count += 1;
            if (count == 3){
                OnEnterEvent.Invoke(other.gameObject);
            }
        }
    }
}
