using System.Globalization;
using TMPro;
using Unity.VRTemplate;
using UnityEngine;

public class PanelChallenge : PanelScreen
{
    private playSteps stepManager;
    public TextMeshProUGUI NumberText;
    public TextMeshProUGUI DisplayText;

    public GameObject DisplayGrandeOurse;
    public GameObject DisplayEtoilePolaire;
    public GameObject DisplayOrion;
    public GameObject DisplayCasiopee;


    void Start()
    {
        stepManager = GameObject.Find("Story").GetComponent<playSteps>();
    }

    public override void UpdateScreen()
    {
    }

    public void UpdateChallengerNumber(string noChallenge)
    {
        NumberText.text = noChallenge;
    }

    public void UpdateChallengeText(string noChallenge, string description, float fontSize =5)
    {
        HideAllDisplays();
        NumberText.text = noChallenge;
        DisplayText.gameObject.SetActive(true);
        DisplayText.text = description;
        DisplayText.fontSize = fontSize;
    }

    public void HideAllDisplays()
    {
        DisplayText.gameObject.SetActive(false);
        DisplayGrandeOurse.SetActive(false);
        DisplayEtoilePolaire.SetActive(false);
        DisplayOrion.SetActive(false);
        DisplayCasiopee.SetActive(false);
    }

    public void ShowGrandeOurse()
    {
        HideAllDisplays();
        DisplayGrandeOurse.SetActive(true);
    }

    public void ShowEtoilePolaire()
    {
        HideAllDisplays();
        DisplayEtoilePolaire.SetActive(true);
    }

    public void ShowOrion()
    {
        HideAllDisplays();
        DisplayOrion.SetActive(true);
    }

    public void ShowCasiopee()
    {
        HideAllDisplays();
        DisplayCasiopee.SetActive(true);
    }
}
