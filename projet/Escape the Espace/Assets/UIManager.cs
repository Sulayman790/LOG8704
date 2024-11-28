using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public PanelScreen currentDisplayedPanel;
    public Button currentSelectedTab;

    public PanelChallenge panelChallenge;
    public PanelConstellation panelConstellation;
    public PanelSettings panelSettings;

    public AudioSource clickAudio;

    private playSteps stepManager;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance)
        {
            throw new Exception("UIManager already present in scene!");
        }

        Instance = this;
    }

    public List<Button> buttons = new(); // Not yet used
    public List<PanelScreen> panels = new();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        stepManager = GameObject.Find("Story").GetComponent<playSteps>();
        buttons[0].interactable = false;
        panelConstellation.gameObject.SetActive(false);
        panelSettings.gameObject.SetActive(false);

        currentDisplayedPanel = panelChallenge;
        currentSelectedTab = buttons[0];
    }

    public void ShowPanel(int index)
    {
        if (currentDisplayedPanel == panels[index]) return;

        clickAudio.Play();

        currentDisplayedPanel.GetComponent<CanvasGroup>().interactable = false;
        currentDisplayedPanel.GetComponent<CanvasGroup>().blocksRaycasts = false;

        currentDisplayedPanel.GetComponent<Fade>().FadeOut();
        panels[index].gameObject.SetActive(true);
        panels[index].GetComponent<Fade>().FadeIn();
        panels[index].UpdateScreen();
        currentDisplayedPanel = panels[index];

        currentDisplayedPanel.GetComponent<CanvasGroup>().interactable = true;
        currentDisplayedPanel.GetComponent<CanvasGroup>().blocksRaycasts = true;

        currentSelectedTab.interactable = true;
        currentSelectedTab = buttons[index];
        buttons[index].interactable = false;
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
        PanelChallenge challengePanel = Instance.panelChallenge;
        audioSource.Play();

        if (index == 1) 
        { 
            challengePanel.UpdateChallengerNumber("Defi #1");
            challengePanel.ShowGrandeOurse(); 
        }
        if (index == 2) 
        {
            ConstellationManager.Instance.AddConstellation("Grande Ourse");
            challengePanel.UpdateChallengeText("Defi #2", "Complêter le moteur.");
        }

        if (index == 3)
        { 
            challengePanel.UpdateChallengerNumber("Defi #3");
            challengePanel.ShowEtoilePolaire(); 
        }
        if (index == 4) 
        {
            ConstellationManager.Instance.AddConstellation("Petite Ourse");
            challengePanel.UpdateChallengeText("Defi #4", "Alimenter le moteur.");
        }
        if (index == 5) 
        { 
            challengePanel.UpdateChallengerNumber("Defi #5");
            challengePanel.ShowOrion(); 
        }
        if (index == 6) 
        {
            ConstellationManager.Instance.AddConstellation("Orion");
            challengePanel.UpdateChallengeText("Defi #6", "Couper les astéroïdes.");
        }
        if (index == 7) 
        { 
            challengePanel.UpdateChallengerNumber("Defi #7");
            challengePanel.ShowCasiopee(); 
        }
        if (index == 8) 
        {
            ConstellationManager.Instance.AddConstellation("Cassiopee");
            challengePanel.UpdateChallengeText("Defi #8", "Activer le téléporteur.");
        }
        if (index == 9)
        {
            challengePanel.UpdateChallengeText("", "Bien joué!\nLe vaisseau est réparé!");
        }
    }

    public void SkipNextStep()
    {
        stepManager.PlayStepIndex(GetLastPlayedStepIndex());
    }
}
