﻿using Assets.Scripts.Utility;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Items
{
    public class Inventory
    {
        private Dictionary<ItemSlot, Item> ivEquippedItems { get; set; }
        private Dictionary<string, Item> ivItems;

        public Inventory()
        {
            ivItems = new Dictionary<string, Item>();
            ivEquippedItems = new Dictionary<ItemSlot, Item>();
        }

        public List<Item> GetEqippedItems()
        {
            return ivEquippedItems.Values.ToList();
        }

        /// <summary>
        /// Equips an item and puts the equipped item in that slot into the inventory
        /// </summary>
        /// <param name="ItemId"></param>
        public void EquipItem(string ItemId)
        {
            var item = ivItems[ItemId];

            if (ivEquippedItems.ContainsKey(item.Slot))
            {
                var equipped = ivEquippedItems[item.Slot];
                ivEquippedItems.Remove(item.Slot);
                ivItems.Add(equipped.ID, equipped);
            }

            ivItems.Remove(ItemId);
            ivEquippedItems.Add(item.Slot, item);
        }

        public void AddItem(Item item)
        {
            if (!ivItems.ContainsKey(item.ID))
                ivItems.Add(item.ID, item);
            else if (ivItems.ContainsKey(item.ID) && ivItems[item.ID].StackSize < ivItems[item.ID].MaxStackSize)
                ivItems[item.ID].StackSize++;
        }

        public void DeleteItem(string id)
        {
            ivItems.Remove(id);
        }
    }
}