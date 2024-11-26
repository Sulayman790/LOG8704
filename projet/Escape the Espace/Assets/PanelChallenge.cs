using TMPro;
using UnityEngine;

public class PanelChallenge : PanelScreen
{
    public TextMeshProUGUI NumberText;
    public TextMeshProUGUI DescriptionText;

    void Start()
    {
        
    }

    public override void UpdateScreen()
    {
    }


    // If too lazy to access the objects themselves above lel
    public void UpdateNumberText(string text)
    {
        NumberText.text = text;
    }

    public void UpdateDescriptionText(string text)
    {
        DescriptionText.text = text;
    }

    public void UpdateDescriptionFontSize(float fontSize)
    {
        DescriptionText.fontSize = fontSize;
    }
}
