using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConstellationManager : MonoBehaviour
{
    public static ConstellationManager Instance;

    private List<Constellation> constellations;

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
        constellations = FindObjectsByType<Constellation>(FindObjectsSortMode.None).ToList();
    }

    void Update()
    {
        
    }

    public void OnCompletedConstellation(Constellation constellation)
    {
        Debug.Log("Wow you completed " + constellation.name + "!");
    }
}
