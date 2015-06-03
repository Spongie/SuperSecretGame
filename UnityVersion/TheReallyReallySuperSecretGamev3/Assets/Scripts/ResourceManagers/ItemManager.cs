using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Assets.Scripts.Utility;

public class ItemManager : MonoBehaviour
{
    public List<Item> AllItems;

    void Start()
    {
        AllItems = new List<Item>();

        var itemFiles = Resources.LoadAll("Items/").Cast<TextAsset>();

        foreach (var itemFile in itemFiles)
        {
            var item = JsonConvert.DeserializeObject<Item>(itemFile.text);
            AllItems.Add(item);
        }

        Logger.Log(string.Format("{0} Items loaded", AllItems.Count()));
    }

    public IEnumerable<Item> AllWeapons
    {
        get { return AllItems.Where(item => item.Slot == ItemSlot.Weapon); }
    }

}
