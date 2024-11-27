using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Breakable : MonoBehaviour
{
    public List<GameObject> breakablePieces;
    public static event System.Action OnBreakableDestroyed;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (var item in breakablePieces){
            item.SetActive(false);
        }
    }

    public void Break(){
        foreach (var item in breakablePieces)
        {
            item.SetActive(true);
            item.transform.parent = null;
        }
        gameObject.SetActive(false);
        OnBreakableDestroyed?.Invoke();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Break();
        }
    }
}
