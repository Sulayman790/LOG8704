using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class ConstellationLine : MonoBehaviour
{
    public Link starLink;

    private LineRenderer lineRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 posA = lineRenderer.GetPosition(0);
        Vector3 posB = lineRenderer.GetPosition(1);

        GameObject gameObject = new GameObject("LineCollider");
        gameObject.layer = LayerMask.NameToLayer("ConstellationLine");
        gameObject.transform.SetParent(this.transform);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.position = (posA + posB) / 2;
        gameObject.transform.LookAt(lineRenderer.GetPosition(1));
        CapsuleCollider collider = gameObject.AddComponent<CapsuleCollider>();
        collider.direction = 2;
        collider.height = Vector3.Distance(posA, posB) * 0.70f;
        collider.radius = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HighlightLink()
    {
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }

    public void UnhighlightLink()
    {
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
    }
}
