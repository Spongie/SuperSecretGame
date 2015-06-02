using UnityEngine;
using System.Collections;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Assets.Scripts.Utility;

public class ItemManager : MonoBehaviour
{
    void Start()
    {
        IEnumerable<string> itemFiles = Resources.LoadAll("Items\\").Cast<string>();

        foreach (var itemFile in itemFiles)
        {
            var item = JsonConvert.DeserializeObject<Item>(itemFile);
            AllItems.Add(item);
        }

        Logger.Log("Items loaded");
    }

    public List<Item> AllItems { get; set; }

    public IEnumerable<Item> AllWeapons
    {
        get { return AllItems.Where(item => item.Slot == ItemSlot.Weapon); }
    }

}
