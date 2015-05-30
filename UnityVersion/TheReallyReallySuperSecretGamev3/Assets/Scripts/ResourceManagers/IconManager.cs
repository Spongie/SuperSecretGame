using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IconManager : MonoBehaviour
{
    void Start()
    {
        var icons = Resources.LoadAll<Sprite>("Icons\\").Cast<Sprite>();

        foreach (var icon in icons)
        {
            Icons.Add(icon.name, icon);
        }
    }

    public Dictionary<string, Sprite> Icons { get; set; }
}
