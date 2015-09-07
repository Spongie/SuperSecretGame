using Assets.Scripts.Utility;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Assets.Scripts.Items
{
    public class Inventory
    {
        private Dictionary<ItemSlot, Item> ivEquippedItems { get; set; }
        public Dictionary<string, Item> Items;

        public Inventory()
        {
            Items = new Dictionary<string, Item>();
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
            var item = Items[ItemId];

            if (ivEquippedItems.ContainsKey(item.Slot))
            {
                var equipped = ivEquippedItems[item.Slot];
                ivEquippedItems.Remove(item.Slot);
                AddItem(equipped);
            }

            DeleteItem(ItemId);
            ivEquippedItems.Add(item.Slot, item);
        }

        public void AddItem(Item item)
        {
            if (!Items.ContainsKey(item.ID))
                Items.Add(item.ID, item);
            else if (Items.ContainsKey(item.ID) && Items[item.ID].StackSize < Items[item.ID].MaxStackSize)
                Items[item.ID].StackSize++;
        }

        public void DeleteItem(string id)
        {
            if (Items.ContainsKey(id))
            {
                if (Items[id].StackSize > 1)
                    Items[id].StackSize--;
                else
                    Items.Remove(id);
            }
        }

        public Item GetEqippedItemAtSlot(ItemSlot piSlot)
        {
            if(ivEquippedItems.ContainsKey(piSlot))
            {
                return ivEquippedItems[piSlot];
            }

            var item = new Item() { Slot = piSlot };
            return item;
        }

        public void UnEquipItemAtSlot(ItemSlot piSlot)
        {
            if(ivEquippedItems.ContainsKey(piSlot))
            {
                var item = GetEqippedItemAtSlot(piSlot);
                ivEquippedItems.Remove(piSlot);

                AddItem(item);
            }
        }
    }
}
