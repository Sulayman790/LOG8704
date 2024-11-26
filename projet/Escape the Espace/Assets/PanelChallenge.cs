using System.Globalization;
using TMPro;
using Unity.VRTemplate;
using UnityEngine;

public class PanelChallenge : PanelScreen
{
    private playSteps stepManager;
    public TextMeshProUGUI NumberText;
    public TextMeshProUGUI DescriptionText;

    void Start()
    {
        stepManager = GameObject.Find("Story").GetComponent<playSteps>();
    }

    public override void UpdateScreen()
    {
    }

    public void UpdateChallengeText(string challenge, string description, float fontSize =5)
    {
        NumberText.text = challenge;
        DescriptionText.text = description;
        DescriptionText.fontSize = fontSize;
    }
}
