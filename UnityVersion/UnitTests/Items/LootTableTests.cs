using Assets.Scripts.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.Items
{
    [TestClass]
    public class LootTableTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void PropertyChangedFired()
        {
            var item = new LootTable();
            item.PropertyChanged += Item_PropertyChanged;
            item.Name = "  ";
        }

        [TestMethod]
        public void GetRollingList_Length100()
        {
            var item = new LootTable();
            item.Items.Add(new LootTableItem() { DropChance = 50 });
            item.Items.Add(new LootTableItem() { DropChance = 50 });

            Assert.AreEqual(item.GetRollingList().Count, 100);
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
