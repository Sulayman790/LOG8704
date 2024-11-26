using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public PanelChallenge panelChallenge;
    public PanelConstellation panelConstellation;
    public PanelSettings panelSettings;

    private playSteps stepManager;

    private void Awake()
    {
        if (Instance)
        {
            throw new Exception("UIManager already present in scene!");
        }

        Instance = this;
    }

    public List<Button> buttons = new List<Button>(); // Not yet used
    public List<PanelScreen> panels = new List<PanelScreen>();

    void Start()
    {
        stepManager = GameObject.Find("Story").GetComponent<playSteps>();
        ShowPanel(0);
    }

    void Update()
    {

    }

    public void ShowPanel(int index)
    {
        foreach (PanelScreen panel in panels) panel.gameObject.SetActive(false);
        panels[index].gameObject.SetActive(true);
        panels[index].UpdateScreen();
    }


    public int GetLastPlayedStepIndex()
    {
        int index = 0;
        foreach (playSteps.Step step in stepManager.steps)
        {
            if (!step.hasPlayed) return index;
            ++index;
        }

        return index;
    }
    public void UpdateChallenge()
    {
        int index = GetLastPlayedStepIndex();
        Debug.Log("I got called!!!! with " + GetLastPlayedStepIndex());
        PanelChallenge panel = UIManager.Instance.panelChallenge;
        if (index == 0) panel.UpdateChallengeText("Defi #1", "Demarre le moteur avec la poele !");
        if (index == 1) panel.UpdateChallengeText("Defi #2", "Trouve la grande orion. Utilise la chose pour bouger");
        if (index == 2) panel.UpdateChallengeText("Defi #3", "Dessine la grande ourse boi.");


    }
}
