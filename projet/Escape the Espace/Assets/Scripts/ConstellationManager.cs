using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    public static ConstellationManager Instance;

    //[HideInInspector]
    //public List<Constellation> constellations;

    // [HideInInspector]
    //public List<Constellation> foundConstellations;

    [HideInInspector]
    public List<string> foundConstellationNames;

    private void Awake()
    {
        if (Instance)
        {
            throw new Exception("ConstellationManager already present in scene!");
        }

        Instance = this;
    }

    void Start()
    {
        //constellations = FindObjectsByType<Constellation>(FindObjectsSortMode.None).ToList();
        //foundConstellations.Add(constellations[0]);
        //foundConstellations.Add(constellations[1]);
        //foundConstellations.Add(constellations[2]);

    }

    void Update()
    {
        
    }

    public void AddConstellation(string name)
    {
        if (!foundConstellationNames.Contains(name)) foundConstellationNames.Add(name);
        UIManager.Instance.panelConstellation.mustNotify = true;
        UIManager.Instance.panelConstellation.UpdateScreen();
    }
}
