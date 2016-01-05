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

        public List<Item> GetEquippedItemsMenu()
        {
            var allEquippedItems = new List<Item>();

            foreach (ItemSlot slot in Enum.GetValues(typeof(ItemSlot)))
            {
                if (slot == ItemSlot.Consumable)
                    continue;

                if (ivEquippedItems.ContainsKey(slot))
                {
                    foreach (var item in ivEquippedItems[slot])
                    {
                        allEquippedItems.Add(item);
                    }

                    if (ivEquippedItems[slot].Count != GetItemMaxEquipAmount(slot))
                    {
                        AddMissingItems(allEquippedItems, slot);
                    }
                }
                else
                    AddMissingItems(allEquippedItems, slot);
            }

            return allEquippedItems;
        }

        private void AddMissingItems(List<Item> allEquippedItems, ItemSlot slot)
        {
            int toAdd;

            if (ivEquippedItems.ContainsKey(slot))
                toAdd = GetItemMaxEquipAmount(slot) - ivEquippedItems[slot].Count;
            else
                toAdd = GetItemMaxEquipAmount(slot);

            for (int i = 0; i < toAdd; i++)
            {
                allEquippedItems.Add(new Item() { Slot = slot, Name = "Empty", IconName = "Empty" });
            }
        }

        /// <summary>
        /// Equips an item and puts the equipped item in that slot into the inventory
        /// </summary>
        /// <param name="ItemId"></param>
        public void EquipItem(string ItemId, string ItemIdToReplace)
        {
            var item = Items[ItemId];
            bool needManuallAdd = true;
            int index = -1;

            if (ivEquippedItems.ContainsKey(item.Slot))
            {
                int itemMax = GetItemMaxEquipAmount(item.Slot);

                var equipped = ivEquippedItems[item.Slot];

                if (equipped.Count < itemMax)
                {
                    equipped.Add(item);
                    needManuallAdd = false;
                }
                else
                {
                    index = UnEquipItemAtSlot(item.Slot, ItemIdToReplace);
                }
            }

            if (needManuallAdd)
            {
                if (!ivEquippedItems.ContainsKey(item.Slot))
                    ivEquippedItems.Add(item.Slot, new List<Item>() { item });
                else
                {
                    if (index == -1)
                        ivEquippedItems[item.Slot].Add(item);
                    else
                        ivEquippedItems[item.Slot].Insert(index, item);
                }
            }

            DeleteItemFromInventory(ItemId);
        }

        public bool HasFreeSlotForItem(Item piItem)
        {
            if (ivEquippedItems.ContainsKey(piItem.Slot))
            {
                int maxAmount = GetItemMaxEquipAmount(piItem.Slot);

                return ivEquippedItems[piItem.Slot].Count < maxAmount;
            }

            return true;
        }

        internal void AddDebugItems()
        {
            Items.Add("1", new Item() { ID = "1", Damage = 20, Name = "Major1", Slot = ItemSlot.MajorGem, Defense = 2, IconName = "Aquamarine Gem05" });
            Items.Add("2", new Item() { ID = "2", Damage = 20, Name = "Minor0", Slot = ItemSlot.MinorGem, Defense = 2, IconName = "Emerald Gem01" });
            Items.Add("3", new Item() { ID = "3", Damage = 20, Name = "Minor1", Slot = ItemSlot.MinorGem, Defense = 2, IconName = "Emerald Gem02" });
            Items.Add("4", new Item() { ID = "4", Damage = 20, Name = "Minor2", Slot = ItemSlot.MinorGem, Defense = 2, IconName = "Emerald Gem03" });
            Items.Add("5", new Item() { ID = "5", Damage = 20, Name = "Minor3", Slot = ItemSlot.MinorGem, Defense = 2, IconName = "Emerald Gem01" });
            Items.Add("6", new Item() { ID = "6", Damage = 20, Name = "Minor4", Slot = ItemSlot.MinorGem, Defense = 2, IconName = "Emerald Gem01" });
            Items.Add("7", new Item() { ID = "7", Damage = 20, Name = "Major2", Slot = ItemSlot.MajorGem, Defense = 2, IconName = "Emerald Gem05" });
        }

        private static int GetItemMaxEquipAmount(ItemSlot slot)
        {
            int itemMax = 1;
            if (slot == ItemSlot.Ring)
                itemMax = 2;
            else if (slot == ItemSlot.MinorGem)
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

        public int UnEquipItemAtSlot(ItemSlot piSlot, string itemId)
        {
            int itemMax = GetItemMaxEquipAmount(piSlot);

            if(ivEquippedItems.ContainsKey(piSlot))
            {
                var item = GetEqippedItemAtSlot(piSlot);

                if (!string.IsNullOrEmpty(itemId))
                {
                    var itemToRemove = item.First(equip => equip.ID == itemId);
                    int index = ivEquippedItems[itemToRemove.Slot].IndexOf(itemToRemove);
                    ivEquippedItems[itemToRemove.Slot].Remove(itemToRemove);
                    AddItem(itemToRemove);

                    return index;
                }
                else
                {
                    AddItem(item.First());
                    ivEquippedItems.Remove(piSlot);
                }
            }

            return -1;
        }
    }
}
