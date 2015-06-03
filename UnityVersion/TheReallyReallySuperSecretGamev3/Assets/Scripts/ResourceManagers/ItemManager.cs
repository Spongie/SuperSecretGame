﻿using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Assets.Scripts.Utility;

public class ItemManager : MonoBehaviour
{
    public List<Item> AllItems;
    public List<LootTable> AllLootTables { get; set; }

    void Start()
    {
        AllItems = new List<Item>();
        AllLootTables = new List<LootTable>();

        var itemFiles = Resources.LoadAll("Items/").Cast<TextAsset>();
        var lootTableFiles = Resources.LoadAll("LootTables/").Cast<TextAsset>();
        
        foreach (var itemFile in itemFiles)
        {
            var item = JsonConvert.DeserializeObject<Item>(itemFile.text);
            AllItems.Add(item);
        }

        foreach (var lootTableFile in lootTableFiles)
        {
            var lootTable = JsonConvert.DeserializeObject<LootTable>(lootTableFile.text);
            AllLootTables.Add(lootTable);
        }

        Logger.Log(string.Format("{0} Items loaded", AllItems.Count()));
        Logger.Log(string.Format("{0} LootTables loaded", AllLootTables.Count()));
    }

    public IEnumerable<Item> AllWeapons
    {
        get { return AllItems.Where(item => item.Slot == ItemSlot.Weapon); }
    }

}
