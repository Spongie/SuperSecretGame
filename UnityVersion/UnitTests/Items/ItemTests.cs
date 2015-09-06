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

        [TestMethod]
        public void GetStats_EmptyStats_ZeroStatsObject()
        {
            var stats = new Item().GetStats();

            Assert.IsTrue(stats.IsZero());
        }

        [TestMethod]
        public void GetStats_DamageSet_StatsWithDamage()
        {
            var item = new Item() { Damage = 10 };
            var stats = item.GetStats();

            Assert.IsTrue(!stats.IsZero());
            Assert.AreEqual(10, stats.Damage);
        }
    }
}
