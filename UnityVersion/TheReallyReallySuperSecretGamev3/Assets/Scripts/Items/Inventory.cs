using Assets.Scripts.Utility;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts.Items
{
    public class Inventory
    {
        private Dictionary<ItemSlot, List<Item>> ivEquippedItems { get; set; }
        public Dictionary<string, Item> Items;

        public Inventory()
        {
            Items = new Dictionary<string, Item>();
            ivEquippedItems = new Dictionary<ItemSlot, List<Item>>();
        }

        public List<Item> GetEqippedItems()
        {
            var allEquippedItems = new List<Item>();

            foreach (var itemList in ivEquippedItems.OrderBy(slot => (int)slot.Key))
            {
                foreach (var item in itemList.Value)
                {
                    allEquippedItems.Add(item);
                }
            }

            return allEquippedItems;
        }

        /// <summary>
        /// Equips an item and puts the equipped item in that slot into the inventory
        /// </summary>
        /// <param name="ItemId"></param>
        public void EquipItem(string ItemId, string ItemIdToReplace)
        {
            var item = Items[ItemId];
            bool needManuallAdd = true;

            if (ivEquippedItems.ContainsKey(item.Slot))
            {
                int itemMax = GetItemMaxEquipAmount(item);

                var equipped = ivEquippedItems[item.Slot];

                if (equipped.Count < itemMax)
                {
                    equipped.Add(item);
                    needManuallAdd = false;
                }
                else
                {
                    UnEquipItemAtSlot(item.Slot, ItemIdToReplace);
                }
            }

            if (needManuallAdd)
            {
                if (!ivEquippedItems.ContainsKey(item.Slot))
                    ivEquippedItems.Add(item.Slot, new List<Item>() { item });
                else
                    ivEquippedItems[item.Slot].Add(item);
            }

            DeleteItemFromInventory(ItemId);
        }

        private static int GetItemMaxEquipAmount(Item item)
        {
            int itemMax = 1;
            if (item.Slot == ItemSlot.Ring)
                itemMax = 2;
            else if (item.Slot == ItemSlot.MinorGem)
                itemMax = 3;
            return itemMax;
        }

        public void AddItem(Item item)
        {
            if (!Items.ContainsKey(item.ID))
                Items.Add(item.ID, item);
            else if (Items.ContainsKey(item.ID) && Items[item.ID].StackSize < Items[item.ID].MaxStackSize)
                Items[item.ID].StackSize++;
        }

        public void DeleteItemFromInventory(string id)
        {
            if (Items.ContainsKey(id))
            {
                if (Items[id].StackSize > 1)
                    Items[id].StackSize--;
                else
                    Items.Remove(id);
            }
        }

        public List<Item> GetEqippedItemAtSlot(ItemSlot piSlot)
        {
            if(ivEquippedItems.ContainsKey(piSlot))
            {
                return ivEquippedItems[piSlot];
            }

            var item = new Item() { Slot = piSlot };
            return new List<Item>() { item };
        }

        public void EquipItem(Item selectedItem, Item replacedItem)
        {
            EquipItem(selectedItem.ID, replacedItem.ID);
        }

        public void UnEquipItemAtSlot(ItemSlot piSlot, string itemId)
        {
            int itemMax = GetItemMaxEquipAmount(new Item() { Slot = piSlot });

            if(ivEquippedItems.ContainsKey(piSlot))
            {
                var item = GetEqippedItemAtSlot(piSlot);

                if (!string.IsNullOrEmpty(itemId))
                {
                    var itemToRemove = item.First(equip => equip.ID == itemId);
                    ivEquippedItems[itemToRemove.Slot].Remove(itemToRemove);
                    AddItem(itemToRemove);
                }
                else
                {
                    AddItem(item.First());
                    ivEquippedItems.Remove(piSlot);
                }
            }
        }
    }
}
