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
}
