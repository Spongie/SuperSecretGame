﻿using UnityEngine;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using Assets.Scripts.Items;

namespace Assets.Scripts.Environment.ResourceManagers
{
    public class ItemManager : MonoBehaviour
    {
        public List<Item> AllItems;
        public List<LootTable> AllLootTables { get; set; }

        void Awake()
        {
            AllLootTables = new List<LootTable>();

            AllItems = Resources.LoadAll<Item>("Items/").ToList();
            AllLootTables = Resources.LoadAll<LootTable>("LootTables/").ToList();

            Utility.Logger.Log(string.Format("{0} Items loaded", AllItems.Count()));
            Utility.Logger.Log(string.Format("{0} LootTables loaded", AllLootTables.Count()));
        }

        public LootTable GetLootTable(string name)
        {
            return AllLootTables.First(table => table.Name.ToLower() == name.ToLower());
        }

        public Item GetItemForDrop(string name)
        {
            Item original = AllItems.First(item => item.Name == name);
            original.StackSize = 1;

            return new Item(original);
        }
    }
}