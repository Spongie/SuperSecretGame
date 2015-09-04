using Assets.Scripts.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.Items
{
    [TestClass]
    public class LootTableItemTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void PropertyChangedFired()
        {
            var item = new LootTableItem();
            item.PropertyChanged += Item_PropertyChanged;
            item.ItemName = "23";
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
