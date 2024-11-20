using UnityEngine;

public class CrystalRemover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        GetComponent<TriggerZone>().OnEnterEvent.AddListener(RemoveCrystal);
    }

    public void RemoveCrystal(GameObject go){
        go.SetActive(false);
    }
}
