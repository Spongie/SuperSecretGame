using UnityEngine;
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