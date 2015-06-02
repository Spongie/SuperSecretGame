using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;

public class IconManager : MonoBehaviour
{
    void Start()
    {
        var icons = Resources.LoadAll<Sprite>("Icons\\").Cast<Sprite>();

        foreach (var icon in icons)
        {
            Icons.Add(icon.name, icon);
        }

        Logger.Log("Icons loaded");
    }

    public Dictionary<string, Sprite> Icons { get; set; }
}
