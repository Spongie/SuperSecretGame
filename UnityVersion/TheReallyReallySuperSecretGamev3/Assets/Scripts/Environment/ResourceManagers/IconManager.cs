using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utility;

namespace Assets.Scripts.Environment.ResourceManagers
{
    public class IconManager : MonoBehaviour
    {
        void Start()
        {
            Icons = new Dictionary<string, Sprite>();

            var icons = Resources.LoadAll<Sprite>("Icons/").Cast<Sprite>();

            foreach (var icon in icons)
            {
                Icons.Add(icon.name, icon);
            }

            Utility.Logger.Log(string.Format("{0} Icons loaded", Icons.Count()));
        }

        public Dictionary<string, Sprite> Icons;
    }
}