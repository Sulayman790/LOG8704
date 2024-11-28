using UnityEngine;

public class CrystalRemover : MonoBehaviour
{
    private TriggerTrash triggerZone;

    private void Start()
    {
        triggerZone = GetComponent<TriggerTrash>();
        if (triggerZone != null)
        {
            triggerZone.OnEnterEvent.AddListener(RemoveCrystal);
        }
    }

    private void RemoveCrystal(GameObject go)
    {
        if (go != null)
        {
            go.SetActive(false);
        }
    }
}