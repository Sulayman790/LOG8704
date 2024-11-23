using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class LaserPistol : MonoBehaviour
{
    public GameObject Laser;
    public float drawingPlaneDistance;

    private LineRenderer lineRenderer;
    public List<Vector3> drawPoints = new List<Vector3>();
    private bool isDrawing = false;
    private GameObject drawingPlane;
    private Constellation currentConstellation = null;
    private List<Link> starLinks = new List<Link>();
    private int lastStarNumber = -1;

    void Start()
    {
        SetupLineRenderer();
        CreateDrawingPlane();
        SetupInteractions();
    }

    void Update()
    {
        UpdateDrawingPlane();
        HandleDrawing();
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
        grabInteractable.selectEntered.AddListener(x => SelectEntered());
        grabInteractable.selectExited.AddListener(x => SelectExited());
        grabInteractable.activated.AddListener(x => Shoot());
        grabInteractable.deactivated.AddListener(x => StopShooting());
    }

    private void UpdateDrawingPlane()
    {
        Vector3 planePosition = transform.position + transform.forward * drawingPlaneDistance;
        drawingPlane.transform.position = planePosition;
        drawingPlane.transform.LookAt(transform.position);
        drawingPlane.transform.Rotate(90, 0, 0);
    }

    private void HandleDrawing()
    {
        if (isDrawing)
        {
            // See if it touches a star
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            LayerMask layerMask = LayerMask.GetMask("Star");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                TryConnectStar(hit);
            }

            UpdateTracerPosition();
        }
    }

    private void TryConnectStar(RaycastHit hit)
    {
        // Check if we're still in the same constellation (or no constellation yet)
        Constellation touchedConstellation = hit.transform.parent.GetComponent<Constellation>();
        if (currentConstellation != null && touchedConstellation != currentConstellation) return;

        int starNumber = Int32.Parse(hit.transform.name);

        // If we're on the first star
        if (lastStarNumber == -1)
        {
            Debug.Log("Star touched!");
            currentConstellation = touchedConstellation; // Set current constellation

            lastStarNumber = starNumber;
            MoveTracerToPosition(hit.transform.position);
            drawPoints.Add(Vector3.zero);

            return;
        }

        // Cancel if we're touching the same star as last one
        if (starNumber == lastStarNumber) return;

        Debug.Log("Star touched!");

        Link currentLink = new Link(lastStarNumber, starNumber);

        // If the link between two stars doesnt exists, connect it.
        if (starLinks.Contains(currentLink) == false)
        {
            starLinks.Add(currentLink);
            MoveTracerToPosition(hit.transform.position);
            drawPoints.Add(Vector3.zero);
            lastStarNumber = starNumber;

            Debug.Log("Added link [" + currentLink.StarA + ", " + currentLink.StarB + "]");
        }

        // Verify if the constellation is completed
        if (isConstellationCorrect())
        {
            ConstellationManager.Instance.OnCompletedConstellation(currentConstellation);
            isDrawing = false;
        }
    }

    private bool isConstellationCorrect()
    {
        if (currentConstellation == null) return false;
        if (currentConstellation.links.Count != starLinks.Count) return false;

        foreach (Link link in currentConstellation.links)
        {
            if (starLinks.Contains(link) == false) return false;
        }

        return true;
    }

    private void MoveTracerToPosition(Vector3 position)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, position - transform.position);
        int layerMask = 1 << LayerMask.NameToLayer("MovableSkybox");
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            drawPoints[drawPoints.Count - 1] = hit.point;
            UpdateDrawing();
        }
    }

    private void UpdateTracerPosition()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        int layerMask = 1 << LayerMask.NameToLayer("MovableSkybox");
        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            drawPoints[drawPoints.Count - 1] = hit.point;
            UpdateDrawing();
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

    private void SelectEntered()
    {
        Laser.SetActive(true);
        lineRenderer.enabled = true;
        drawingPlane.GetComponent<Collider>().enabled = true;
    }

    private void SelectExited()
    {
        ResetDrawings();
        Laser.SetActive(false);
        lineRenderer.enabled = false;
        drawingPlane.GetComponent<Collider>().enabled = false;
    }

    private void Shoot()
    {
        ResetDrawings();
        isDrawing = true;
    }

    private void StopShooting()
    {
        isDrawing = false;
    }

    private void ResetDrawings()
    {
        currentConstellation = null;
        starLinks.Clear();
        lastStarNumber = -1;
        drawPoints = new List<Vector3> { Vector3.zero };
    }
}