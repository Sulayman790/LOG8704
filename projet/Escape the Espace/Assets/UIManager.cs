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

    public List<Button> buttons = new List<Button>(); // Not yet used
    public List<PanelScreen> panels = new List<PanelScreen>();

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        stepManager = GameObject.Find("Story").GetComponent<playSteps>();
        buttons[0].interactable = false;
        panelConstellation.gameObject.SetActive(false);
        panelSettings.gameObject.SetActive(false);

        currentDisplayedPanel = panelChallenge;
        currentSelectedTab = buttons[0];
    }

    void Update()
    {

    }

    public void ShowPanel(int index)
    {
        if (currentDisplayedPanel == panels[index]) return;
        currentDisplayedPanel.GetComponent<Fade>().FadeOut();
        panels[index].gameObject.SetActive(true);
        panels[index].GetComponent<Fade>().FadeIn();
        panels[index].UpdateScreen();
        currentDisplayedPanel = panels[index];

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

    // 10x enginner code right there
    public void UpdateChallenge()
    {
        int index = GetLastPlayedStepIndex();
        PanelChallenge challengePanel = UIManager.Instance.panelChallenge;
        if (index == 0) 
        {
            challengePanel.UpdateChallengeText("Defi #1", "Pese le boutton rouge !");
            return;
         };

        audioSource.Play();

        if (index == 1) 
        { 
            challengePanel.UpdateChallengerNumber("Defi #2"); 
            challengePanel.ShowGrandeOurse(); 
        }
        if (index == 2) 
        {
            ConstellationManager.Instance.AddConstellation("Grande Ourse");
            challengePanel.UpdateChallengeText("Defi #3", "Alimente le moteur avec la casserole .");
        };
        if (index == 3) 
        { 
            challengePanel.UpdateChallengerNumber("Defi #4"); 
            challengePanel.ShowEtoilePolaire(); 
        }
        if (index == 4) 
        {
            ConstellationManager.Instance.AddConstellation("Petite Ourse");
            challengePanel.UpdateChallengeText("Defi #5", "Alimente le moteur avec les cristaux.");
        };
        if (index == 5) 
        { 
            challengePanel.UpdateChallengerNumber("Defi #6"); 
            challengePanel.ShowOrion(); 
        }
        if (index == 6) 
        {
            ConstellationManager.Instance.AddConstellation("Orion");
            challengePanel.UpdateChallengeText("Defi #7", "Brise les asteorides"); 
        }
        if (index == 7) 
        { 
            challengePanel.UpdateChallengerNumber("Defi #8"); 
            challengePanel.ShowCasiopee(); 
        }
        if (index == 8) 
        {
            ConstellationManager.Instance.AddConstellation("Cassiopee");
            challengePanel.UpdateChallengeText("Defi #9", "Alimente la machine avec l'energie.");
        };
    }
}
