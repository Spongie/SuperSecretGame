using Assets.Scripts.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.Items
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void PropertyChangedFired()
        {
            var item = new Item();
            item.PropertyChanged += Item_PropertyChanged;
            item.Defense = 23;
        }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
