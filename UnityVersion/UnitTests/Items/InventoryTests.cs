using Assets.Scripts.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests.Items
{
    [TestClass]
    public class InventoryTests
    {
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
            ivInventory.DeleteItem("asd");

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

            ivInventory.DeleteItem(item.ID);

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

            ivInventory.DeleteItem(item.ID);

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

            ivInventory.EquipItem(item.ID);

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

            ivInventory.EquipItem(item.ID);
            ivInventory.EquipItem(item2.ID);

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

            ivInventory.EquipItem(item.ID);
            ivInventory.EquipItem(item2.ID);

            Assert.AreEqual(2, ivInventory.Items.Values.First().StackSize);
        }

        [TestMethod]
        public void GetEquippedItemAtSlot_NeverNull()
        {
            var item = ivInventory.GetEqippedItemAtSlot(ItemSlot.Chest);

            Assert.IsTrue(item != null);
        }

        [TestMethod]
        public void GetEquippedItemAtSlot_NoItem_EmptyItemSameSlot()
        {
            var item = ivInventory.GetEqippedItemAtSlot(ItemSlot.Chest);

            Assert.IsTrue(item.Slot == ItemSlot.Chest);
        }

        [TestMethod]
        public void GetEquippedItemAtSlot_NoItem_NoStats()
        {
            var item = ivInventory.GetEqippedItemAtSlot(ItemSlot.Chest);

            Assert.IsTrue(item.GetStats().IsZero());
        }
    }
}