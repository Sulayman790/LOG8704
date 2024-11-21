using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LaserPistol : MonoBehaviour
{
    public GameObject Laser;
    private LineRenderer lineRenderer;
    private List<Vector3> drawPoints = new List<Vector3>();
    private bool isDrawing = false;
    private GameObject drawingPlane;

    void Start()
    {
        SetupLineRenderer();
        CreateDrawingPlane();
        SetupInteractions();
    }

    private void SetupLineRenderer()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        if (lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Configure LineRenderer
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.enabled = false;

        // Create and set material
        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));
        lineMaterial.color = Color.green; // Set your desired color
        lineRenderer.material = lineMaterial;

        // Enable color gradient
        lineRenderer.useWorldSpace = true;
        lineRenderer.colorGradient = new Gradient()
        {
            colorKeys = new GradientColorKey[] {
            new GradientColorKey(Color.green, 0.0f),
            new GradientColorKey(Color.green, 1.0f)
        },
            alphaKeys = new GradientAlphaKey[] {
            new GradientAlphaKey(1.0f, 0.0f),
            new GradientAlphaKey(1.0f, 1.0f)
        }
        };
    }

    private void CreateDrawingPlane()
    {
        drawingPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        drawingPlane.layer = LayerMask.NameToLayer("MovableSkybox");
        drawingPlane.GetComponent<MeshRenderer>().enabled = false;
        drawingPlane.GetComponent<Collider>().enabled = false;
        drawingPlane.transform.localScale = new Vector3(0.3f, 1f, 0.3f);
    }

    private void SetupInteractions()
    {
        var grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => Shoot());
        grabInteractable.deactivated.AddListener(x => StopShooting());
    }

    void Update()
    {
        UpdateDrawingPlane();
        HandleDrawing();
    }

    private void UpdateDrawingPlane()
    {
        Vector3 planePosition = transform.position + transform.forward * 2f;
        drawingPlane.transform.position = planePosition;
        drawingPlane.transform.LookAt(transform.position);
        drawingPlane.transform.Rotate(90, 0, 0);
    }

    private void HandleDrawing()
    {
        if (isDrawing)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            int layerMask = 1 << LayerMask.NameToLayer("MovableSkybox");

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                drawPoints.Add(hit.point);
                UpdateDrawing();
            }
        }
    }

    private void UpdateDrawing()
    {
        if (drawPoints.Count > 0)
        {
            lineRenderer.positionCount = drawPoints.Count;
            lineRenderer.SetPositions(drawPoints.ToArray());
        }
    }

    public void Shoot()
    {
        isDrawing = true;
        Laser.SetActive(true);
        lineRenderer.enabled = true;
        drawingPlane.GetComponent<Collider>().enabled = true;
        drawPoints.Clear();
    }

    public void StopShooting()
    {
        isDrawing = false;
        Laser.SetActive(false);
        lineRenderer.enabled = false;
        drawingPlane.GetComponent<Collider>().enabled = false;
    }

    private void OnActivated(ActivateEventArgs args)
    {
        Shoot();
    }

    private void OnDeactivated(DeactivateEventArgs args)
    {
        StopShooting();
    }
}