using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Constellation : MonoBehaviour
{
    public String nomConstellation;
    public List<Link> links;


    // Cette fonction est appelé seulement en mode Editeur
    // Pour debogger les constellations
    private void OnDrawGizmos()
    {
        if (links == null || links.Count == 0) return;

        Gizmos.color = Color.red;
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child.transform);
        }


        foreach (Link link in links)
        {
            Transform starA = children.FirstOrDefault(gameObj => Int32.Parse(gameObj.name) == link.StarA);
            Transform starB = children.FirstOrDefault(gameObj => Int32.Parse(gameObj.name) == link.StarB);

            if (starA != null && starB != null)
            {
                Gizmos.DrawLine(starA.position, starB.position);
            }
        }
    }
}

[System.Serializable]
public class Link
{
    public int StarA;
    public int StarB;

    public Link(int starA, int starB)
    {
        StarA = starA;
        StarB = starB;
    }

    public override bool Equals(object obj)
    {
        var item = obj as Link;

        if (item == null)
        {
            return false;
        }

        return (StarA == item.StarA && StarB == item.StarB) || (StarA == item.StarB && StarB == item.StarA);
    }

    public override int GetHashCode()
    {
        return this.StarA.GetHashCode() ^ this.StarB.GetHashCode();
    }
}
