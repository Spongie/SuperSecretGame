using Assets.Scripts.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests.Items
{
    [TestClass]
    public class InventoryTests
    {
        /*
        private Inventory ivInventory;

        [TestInitialize]
        public void Initialize()
        {
            ivInventory = new Inventory();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ivInventory = null;
        }

        [TestMethod]
        public void AddItem_EmptyInventory_ItemAdded()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);

            Assert.AreEqual(1, ivInventory.Items.Count);
        }

        [TestMethod]
        public void AddItem_SameItemExists_ItemStacked()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);
            ivInventory.AddItem(item);

            Assert.AreEqual(1, ivInventory.Items.Count);
            Assert.AreEqual(2, ivInventory.Items.First().Value.StackSize);
        }

        [TestMethod]
        public void AddItem_SameItemExists_MaxStacked_ItemIsThrownAway()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);
            ivInventory.AddItem(item);
            ivInventory.AddItem(item);

            Assert.AreEqual(1, ivInventory.Items.Count);
            Assert.AreEqual(2, ivInventory.Items.First().Value.StackSize);
        }

        [TestMethod]
        public void DeleteItem_NoItem_Nothing()
        {
            ivInventory.DeleteItemFromInventory("asd");

            Assert.AreEqual(0, ivInventory.Items.Count);
        }

        [TestMethod]
        public void DeleteItem_ItemExists_ItemDeleted()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);

            Assert.AreEqual(1, ivInventory.Items.Count);

            ivInventory.DeleteItemFromInventory(item.ID);

            Assert.AreEqual(0, ivInventory.Items.Count);
        }

        [TestMethod]
        public void DeleteItem_ItemExistsStacked_StackRemoved()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);
            ivInventory.AddItem(item);

            Assert.AreEqual(1, ivInventory.Items.Count);

            ivInventory.DeleteItemFromInventory(item.ID);

            Assert.AreEqual(1, ivInventory.Items.Count);
            Assert.AreEqual(1, ivInventory.Items.First().Value.StackSize);
        }

        [TestMethod]
        public void EquipItem_NoEquipped_ItemEquipped()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);

            ivInventory.EquipItem(item.ID, null);

            Assert.AreEqual(1, ivInventory.GetEqippedItems().Count);
        }

        [TestMethod]
        public void EquipItem_Equipped_ItemSwapped()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            var item2 = new Item()
            {
                ID = "sdasd",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);
            ivInventory.AddItem(item2);

            ivInventory.EquipItem(item.ID, null);
            ivInventory.EquipItem(item2.ID, null);

            Assert.AreEqual(1, ivInventory.GetEqippedItems().Count);
            Assert.AreEqual("Asdasda", ivInventory.Items.First().Value.ID);
            Assert.AreEqual("sdasd", ivInventory.GetEqippedItems().First().ID);
        }

        [TestMethod]
        public void EquipItem_Equipped_ItemSwapped_Stacks()
        {
            var item = new Item()
            {
                ID = "Asdasda",
                MaxStackSize = 2
            };

            var item2 = new Item()
            {
                ID = "sdasd",
                MaxStackSize = 2
            };

            ivInventory.AddItem(item);
            ivInventory.AddItem(item);
            ivInventory.AddItem(item2);

            ivInventory.EquipItem(item.ID, null);
            ivInventory.EquipItem(item2.ID, null);

            Assert.AreEqual(2, ivInventory.Items.Values.First().StackSize);
        }

        [TestMethod]
        public void GetEquippedItemAtSlot_NeverNull()
        {
            var item = ivInventory.GetEqippedItemAtSlot(ItemSlot.Neck);

            Assert.IsTrue(item != null);
        }

        [TestMethod]
        public void GetEquippedItemAtSlot_NoItem_EmptyItemSameSlot()
        {
            var item = ivInventory.GetEqippedItemAtSlot(ItemSlot.Neck);

            Assert.IsTrue(item.First().Slot == ItemSlot.Neck);
        }

        [TestMethod]
        public void GetEquippedItemAtSlot_NoItem_NoStats()
        {
            var item = ivInventory.GetEqippedItemAtSlot(ItemSlot.Neck);

            //Assert.IsTrue(item.First().GetStats().IsZero());
        }

        [TestMethod]
        public void GetEquippedItemAtSlot_ItemEquipped_Returned()
        {
            var item = new Item() { Slot = ItemSlot.Neck, ID = "Test" };

            ivInventory.AddItem(item);
            ivInventory.EquipItem(item.ID, null);

            Assert.AreEqual(item, ivInventory.GetEqippedItemAtSlot(ItemSlot.Neck).First());
        }

        [TestMethod]
        public void UnEquipItemAtSlot_ItemAddedToInventory()
        {
            var item = new Item() { Slot = ItemSlot.Neck, ID="Test" };

            ivInventory.AddItem(item);
            ivInventory.EquipItem(item.ID, null);
            ivInventory.UnEquipItemAtSlot(ItemSlot.Neck, null);

            Assert.AreEqual(1, ivInventory.Items.Count);
            //Assert.IsTrue(ivInventory.GetEqippedItemAtSlot(ItemSlot.Neck).First().GetStats().IsZero());
        }

        [TestMethod]
        public void EquipItem_SecondMinorGem_AddedToList()
        {
            var item = new Item() { Slot = ItemSlot.MinorGem, ID = "Test" };
            var item2 = new Item() { Slot = ItemSlot.MinorGem, ID = "Test1" };

            ivInventory.AddItem(item);
            ivInventory.AddItem(item2);

            ivInventory.EquipItem(item.ID, null);
            ivInventory.EquipItem(item2.ID, null);

            Assert.AreEqual(0, ivInventory.Items.Count);
            Assert.AreEqual(2, ivInventory.GetEqippedItemAtSlot(ItemSlot.MinorGem).Count);
        }

        [TestMethod]
        public void EquipItem_ThirdMinorGem_AddedToList()
        {
            SetThreeGemInventory();

            Assert.AreEqual(0, ivInventory.Items.Count);
            Assert.AreEqual(3, ivInventory.GetEqippedItemAtSlot(ItemSlot.MinorGem).Count);
        }

        [TestMethod]
        public void EquipItem_MinorGem_ReplaceAnotherGem()
        {
            SetThreeGemInventory();

            var item = new Item() { Slot = ItemSlot.MinorGem, ID = "Replacer" };
            ivInventory.AddItem(item);

            ivInventory.EquipItem(item.ID, "Test");

            Assert.AreEqual(1, ivInventory.Items.Count);
            Assert.AreEqual(3, ivInventory.GetEqippedItemAtSlot(ItemSlot.MinorGem).Count);
            Assert.AreEqual("Test", ivInventory.Items.First().Value.ID);
        }

        [TestMethod]
        public void HasFreeSlotForItem_NoEquips_True()
        {
            Assert.IsTrue(ivInventory.HasFreeSlotForItem(new Item() { Slot = ItemSlot.MajorGem }));
        }

        [TestMethod]
        public void HasFreeSlotForItem_OneEquips_True()
        {
            var item = new Item() { Slot = ItemSlot.MinorGem, ID = "Test" };
            ivInventory.AddItem(item);
            ivInventory.EquipItem(item.ID, null);

            Assert.IsTrue(ivInventory.HasFreeSlotForItem(new Item() { Slot = ItemSlot.MinorGem }));
        }

        [TestMethod]
        public void HasFreeSlotForItem_FullEquips_False()
        {
            SetThreeGemInventory();

            Assert.IsFalse(ivInventory.HasFreeSlotForItem(new Item() { Slot = ItemSlot.MinorGem }));
        }

        private void SetThreeGemInventory()
        {
            var item = new Item() { Slot = ItemSlot.MinorGem, ID = "Test" };
            var item2 = new Item() { Slot = ItemSlot.MinorGem, ID = "Test1" };
            var item3 = new Item() { Slot = ItemSlot.MinorGem, ID = "Test12" };

            ivInventory.AddItem(item);
            ivInventory.AddItem(item2);
            ivInventory.AddItem(item3);

            ivInventory.EquipItem(item.ID, null);
            ivInventory.EquipItem(item2.ID, null);
            ivInventory.EquipItem(item3.ID, null);
        }*/
    }
}