using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using UnityEngine.Events;

public class LaserPistol : MonoBehaviour
{
    playSteps stepsManager;
    public GameObject movableSkybox;
    public GameObject Laser;
    private Shader shader;
    public Material indicatorMaterial;

    public AudioSource linkAudioSource;
    public AudioSource unlinkAudioSource;


    private GameObject pointIndicator;
    private LineRenderer lineRenderer;

    private ConstellationLine highlightedConstellationLine = null;
    private Constellation currentConstellation = null;

    private List<Link> currentLinks = new List<Link>();
    private List<ConstellationLine> constellationLines = new List<ConstellationLine>();

    private int lastStarNumber = -1;

    private bool isSelected = false;
    private bool isDrawing = false;

    private bool orionFound = false;
    private bool cassiopeiaFound = false;


    void Start()
    {
        stepsManager = GameObject.Find("Story").GetComponent<playSteps>();
        shader = Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply");
        movableSkybox = GameObject.Find("Movable Skybox");
        CreatePointIndicator();
        SetupInteractions();
    }

    void Update()
    {
        HandleLineHovering();
        HandleDrawing();
        UpdateTracerPosition();
    }

    private void CreateNewLine(Vector3 position)
    {
        GameObject gameObj = new GameObject("Line");
        gameObj.transform.SetParent(movableSkybox.transform);
        lineRenderer = gameObj.AddComponent<LineRenderer>();


        // Configure LineRenderer
        lineRenderer.startWidth = 2f;
        lineRenderer.endWidth = 2f;
        lineRenderer.positionCount = 2;

        Vector3[] positions = { position, position };
        lineRenderer.SetPositions(positions);

        // Create and set material
        lineRenderer.material = new Material(shader);


        // Enable color gradient
        lineRenderer.useWorldSpace = false;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
    }

    private void CreatePointIndicator()
    {
        pointIndicator = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        pointIndicator.name = "Point Indicator";
        pointIndicator.transform.position = Vector3.one * 8000; // Randomly very far lel
        pointIndicator.transform.localScale = Vector3.one * 3f; // 0.5f;
        pointIndicator.GetComponent<Renderer>().material = indicatorMaterial;
        pointIndicator.SetActive(false);
    }

    private void SetupInteractions()
    {
        var grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(x => SelectEntered());
        grabInteractable.selectExited.AddListener(x => SelectExited());
        grabInteractable.activated.AddListener(x => Shoot());
        grabInteractable.deactivated.AddListener(x => StopShooting());
    }

    private void HandleDrawing()
    {
        if (!isDrawing) return;

        // See if it touches a star
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        LayerMask layerMask = LayerMask.GetMask("Star");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            TryConnectStar(hit);
        }
    }

    private void HandleLineHovering()
    {
        if (!isSelected || isDrawing) return;

        // See if it touches a line
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        LayerMask layerMask = LayerMask.GetMask("ConstellationLine");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            ConstellationLine touchedConstellationLine = hit.transform.parent.gameObject.GetComponent<ConstellationLine>();
            if (highlightedConstellationLine != null && touchedConstellationLine != highlightedConstellationLine)
            {
                highlightedConstellationLine.UnhighlightLink();
            }
            highlightedConstellationLine = touchedConstellationLine;
            highlightedConstellationLine.HighlightLink();
        }
        else if (highlightedConstellationLine != null)
        {
            highlightedConstellationLine.UnhighlightLink();
            highlightedConstellationLine = null;
        }
    }

    private void CancelConnection()
    {
        if (lineRenderer == null) return;
        GameObject.Destroy(lineRenderer.gameObject);
        lineRenderer = null;
    }

    private void TryConnectStar(RaycastHit hit)
    {
        // Check if we're still in the same constellation (or no constellation yet)
        Constellation touchedConstellation = hit.transform.parent.GetComponent<Constellation>();
        if (lastStarNumber == -1)
        {
            // Don't allow redrawing those if they already found (ghetto code)
            if ((touchedConstellation.name == "Orion" && orionFound) ||
            (touchedConstellation.name == "Cassiopee" && cassiopeiaFound)) return;

            if (currentConstellation != null && currentConstellation != touchedConstellation)
            {
                ResetConstellationProgress();
            }
            currentConstellation = touchedConstellation;
        }
        else if (touchedConstellation != currentConstellation) return;

        int starNumber = Int32.Parse(hit.transform.name);

        // If we're on the first star
        if (lastStarNumber == -1)
        {
            currentConstellation = touchedConstellation; // Set current constellation

            lastStarNumber = starNumber;

            CreateNewLine(hit.transform.position);

            return;
        }

        // Cancel if we're touching the same star as last one
        if (starNumber == lastStarNumber) return;

        Link currentLink = new Link(lastStarNumber, starNumber);

        // If the link between two stars doesnt exists, connect it.
        if (currentLinks.Contains(currentLink) == false)
        {
            currentLinks.Add(currentLink);
            lineRenderer.SetPosition(1, hit.transform.position);
            ConstellationLine constellationLine = lineRenderer.gameObject.AddComponent<ConstellationLine>();
            constellationLines.Add(constellationLine);
            constellationLine.starLink = currentLink;

            CreateNewLine(hit.transform.position);
            lastStarNumber = starNumber;

            linkAudioSource.Play();
        }

        VerifyConstellationCompletion();
    }

    private void VerifyConstellationCompletion()
    {
        if (isConstellationCorrect())
        {
            if (CheckConstellation())
            {
                LockAllLinksAsSucess();
                ConstellationManager.Instance.AddConstellation(currentConstellation.name);
                isDrawing = false;
            };
        }
    }

    private void LockAllLinksAsSucess()
    {
        foreach (ConstellationLine line in constellationLines)
        {
            LineRenderer renderer = line.GetComponent<LineRenderer>();
            renderer.startColor = Color.yellow;
            renderer.endColor = Color.yellow;

            if (line.gameObject.transform.childCount == 1) line.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Destroy(line);
        }

        currentLinks.Clear();
        constellationLines.Clear();
    }

    private bool isConstellationCorrect()
    {
        if (currentConstellation == null) return false;
        if (currentConstellation.links.Count != currentLinks.Count) return false;

        foreach (Link link in currentConstellation.links)
        {
            if (currentLinks.Contains(link) == false) return false;
        }

        return true;
    }

    private void UpdateTracerPosition()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        int layerMask = 1 << LayerMask.NameToLayer("MovableSkybox");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            pointIndicator.transform.position = hit.point;
            if (lineRenderer != null && isDrawing) lineRenderer.SetPosition(1, hit.point);
        }
    }

    private void SelectEntered()
    {
        isSelected = true;
        pointIndicator.SetActive(true);
    }

    private void SelectExited()
    {
        isSelected = false;
        CancelConnection();
        lastStarNumber = -1;
        pointIndicator.SetActive(false);

        if (highlightedConstellationLine != null)
        {
            highlightedConstellationLine.UnhighlightLink();
            highlightedConstellationLine = null;
        }
    }

    private void ResetConstellationProgress()
    {
        foreach (ConstellationLine line in constellationLines)
        {
            GameObject.Destroy(line.gameObject);
        }
        currentLinks.Clear();
        constellationLines.Clear();
    }

    private void Shoot()
    {
        lastStarNumber = -1;
        Laser.SetActive(true);

        if (highlightedConstellationLine != null)
        {
            // Remove link, remove line, destroy line
            currentLinks.Remove(highlightedConstellationLine.starLink);
            constellationLines.Remove(highlightedConstellationLine);
            GameObject.Destroy(highlightedConstellationLine.gameObject);
            highlightedConstellationLine = null;
            unlinkAudioSource.Play();
            VerifyConstellationCompletion();
        }
        else isDrawing = true;
    }

    private void StopShooting()
    {
        CancelConnection();
        isDrawing = false;
        Laser.SetActive(false);
    }

    private bool CheckConstellation() {
        if (CheckConstellationName("Orion", 4, 5)) return true;
        if (CheckConstellationName("Cassiopee", 6, 7)) return true;
        return false;
    }

    private bool CheckConstellationName(string constellationName, int currentSteps, int nextSteps)
    {
        if (stepsManager.steps[currentSteps].hasPlayed && currentConstellation.name == constellationName)
        {
            if (constellationName == "Orion") orionFound = true;
            if (constellationName == "Cassiopee") cassiopeiaFound = true;

            stepsManager.PlayStepIndex(nextSteps);
            return true;
        }
        return false;
    }
}