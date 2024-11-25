using TMPro;
using UnityEngine;

public class PanelConstellation : PanelScreen
{
    public TextMeshProUGUI contellationText;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void UpdateScreen()
    {
        contellationText.text = "Nb Constellations: " + ConstellationManager.Instance.constellations.Count.ToString();
    }
}
