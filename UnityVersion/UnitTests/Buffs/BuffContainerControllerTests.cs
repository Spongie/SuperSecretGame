using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stats;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Buffs
{
    [TestClass]
    public class BuffContainerControllerTests
    {
        private BuffContainerController ivController;

        [TestInitialize]
        public void Initialize()
        {
            ivController = new BuffContainerController();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ivController = null;
        }

        [TestMethod]
        public void ApplyBuff_BuffAdded()
        {
            ivController.ApplyBuff(new Buff(new CStats()));

            Assert.AreEqual(1, ivController.Buffs.Count);
        }

        [TestMethod]
        public void ClearBuff_EmptyList_NoException()
        {
            try
            {
                ivController.ClearBuff(new Buff(new CStats()));
            }
            catch
            {
                Assert.Fail();
            }

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ClearBuff_BuffRemoved()
        {
            var buff = new Buff(new CStats());

            ivController.ApplyBuff(buff);
            ivController.ClearBuff(buff);

            Assert.AreEqual(0, ivController.Buffs.Count);
        }

        [TestMethod]
        public void SetStats_StatsSet()
        {
            var stats = new CStats(23);

            ivController.SetStats(stats);

            Assert.AreEqual(ivController.Stats, stats);
        }

        [TestMethod]
        public void GetBuffStats_NoBuffs_EmptyStats()
        {
            var stats = ivController.GetBuffStats();

            Assert.AreEqual(0, stats.MaximumHealth);
            Assert.AreEqual(0, stats.Damage);
            Assert.AreEqual(0, stats.Defense);
            Assert.AreEqual(0, stats.MagicDamage);
        }

        [TestMethod]
        public void GetBuffStats_2Buffs_CombinedStats()
        {
            var stats = new CStats(23);
            var buff = new Buff(stats);

            ivController.ApplyBuff(buff);
            ivController.ApplyBuff(buff);

            var RealStats = ivController.GetBuffStats();

            Assert.AreEqual(46, RealStats.MaximumHealth);
            Assert.AreEqual(46, RealStats.Damage);
            Assert.AreEqual(46, RealStats.Defense);
            Assert.AreEqual(46, RealStats.MagicDamage);
        }

        [TestMethod]
        public void IsStunned_NoBuff_False()
        {
            var buff = new StunBuff(new CStats(0));


            Assert.IsFalse(ivController.IsStunned());
        }

        [TestMethod]
        public void IsStunned_StunBuff_True()
        {
            var buff = new StunBuff(new CStats(0));

            ivController.ApplyBuff(buff);

            Assert.IsTrue(ivController.IsStunned());
        }

        [TestMethod]
        public void IsFeared_NoBuff_False()
        {
            var buff = new FearBuff(new CStats(0));


            Assert.IsFalse(ivController.IsFeared());
        }

        [TestMethod]
        public void IsFeared_StunBuff_True()
        {
            var buff = new FearBuff(new CStats(0));

            ivController.ApplyBuff(buff);

            Assert.IsTrue(ivController.IsFeared());
        }

        [TestMethod]
        public void IsChilled_NoBuff_False()
        {
            var buff = new ChilledBuff(new CStats(0));


            Assert.IsFalse(ivController.IsChilled());
        }

        [TestMethod]
        public void IsChilled_StunBuff_True()
        {
            var buff = new ChilledBuff(new CStats(0));

            ivController.ApplyBuff(buff);

            Assert.IsTrue(ivController.IsChilled());
        }
    }
}
