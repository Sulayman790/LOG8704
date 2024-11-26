using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelConstellation : PanelScreen
{
    public GameObject panelGrandeOurse;
    public GameObject panelOrion;
    public GameObject panelCassiopee;
    public GameObject panelGrandChien;
    public GameObject panelNoConstellations;

    public Button leftButton;
    public Button rightButton;
    public TextMeshProUGUI indexText;

    private int pageIndex = -1;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void UpdateScreen()
    {
        if (pageIndex == -1 && ConstellationManager.Instance.foundConstellationNames.Count != 0) pageIndex = 0;
        ShowPanelFromIndex();
    }



    public void LeftArrowPressed()
    {
        if (pageIndex <= 0) return;
        pageIndex -= 1;

        ShowPanelFromIndex();
    }

    public void RightArrowPressed()
    {
        if (pageIndex > ConstellationManager.Instance.foundConstellationNames.Count) return;
        pageIndex += 1;

        ShowPanelFromIndex();
    }

    private void ShowPanelFromIndex()
    {
        List<string> foundConstellations = ConstellationManager.Instance.foundConstellationNames;
        leftButton.interactable = pageIndex > 0;
        rightButton.interactable = (pageIndex != -1) && pageIndex < foundConstellations.Count -1;
        indexText.text = (pageIndex + 1) + "/" + foundConstellations.Count;

        HideAllPanels();
        if (pageIndex == -1)
        {
            panelNoConstellations.SetActive(true);
        }
        else
        {
            string constellationName = foundConstellations[pageIndex];
            if (constellationName == "Grande Ourse") panelGrandeOurse.SetActive(true);
            if (constellationName == "Petite Ourse") panelGrandChien.SetActive(true);
            if (constellationName == "Orion") panelOrion.SetActive(true);
            if (constellationName == "Cassiopee") panelCassiopee.SetActive(true);
        }

    }

    private void ShowNoConstellationsPanel()
    {
        panelNoConstellations.SetActive(true);
    }

    private void HideAllPanels()
    {
        panelGrandeOurse.SetActive(false);
        panelOrion.SetActive(false);
        panelCassiopee.SetActive(false);
        panelGrandChien.SetActive(false);
        panelNoConstellations.SetActive(false);
    }
}
