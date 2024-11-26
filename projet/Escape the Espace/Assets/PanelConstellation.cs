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
        if (pageIndex == -1 && ConstellationManager.Instance.foundConstellations.Count != 0) pageIndex = 0;
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
        if (pageIndex > ConstellationManager.Instance.foundConstellations.Count) return;
        pageIndex += 1;

        ShowPanelFromIndex();
    }

    private void ShowPanelFromIndex()
    {
        List<Constellation> foundConstellations = ConstellationManager.Instance.foundConstellations;
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
            Constellation constellation = foundConstellations[pageIndex];
            if (constellation.name == "Grande Ourse") panelGrandeOurse.SetActive(true);
            if (constellation.name == "Orion") panelOrion.SetActive(true);
            if (constellation.name == "Cassiopee") panelCassiopee.SetActive(true);
            if (constellation.name == "Petite Ourse") panelGrandChien.SetActive(true);
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
