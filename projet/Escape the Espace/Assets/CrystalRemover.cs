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
        else
        {
            Debug.LogError("No TriggerZone component found on this GameObject.");
        }
    }

    private void RemoveCrystal(GameObject go)
    {
        if (go != null)
        {
            go.SetActive(false);
            Debug.Log($"GameObject {go.name} has been disabled.");
        }
        else
        {
            Debug.LogWarning("RemoveCrystal called with a null GameObject.");
        }
    }
}