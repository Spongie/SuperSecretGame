using Assets.Scripts.Character.Stat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Character.Stats
{
    [TestClass]
    public class CStatsTests
    {
        private CStats ivStats;

        [TestInitialize]
        public void Initialize()
        {
            ivStats = new CStats(100);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ivStats = null;
        }

        [TestMethod]
        public void DealDamage_HealthReduced()
        {
            ivStats.DealDamage(50);

            Assert.AreEqual(50, ivStats.CurrentHealth);
        }

        [TestMethod]
        public void DealDamage_HealthReducedBelowZero_HealthIsZero()
        {
            ivStats.DealDamage(550);

            Assert.AreEqual(0, ivStats.CurrentHealth);
        }

        [TestMethod]
        public void DealDamage_PercentageCalculated()
        {
            ivStats.DealDamage(50);

            Assert.AreEqual(0.5f, ivStats.HealthPercentage);
        }

        [TestMethod]
        public void DrainMana_HealthReduced()
        {
            ivStats.DrainMana(50);

            Assert.AreEqual(50, ivStats.CurrentMana);
        }

        [TestMethod]
        public void DrainMana_ManaReducedBelowZero_HealthIsZero()
        {
            ivStats.DrainMana(550);

            Assert.AreEqual(0, ivStats.CurrentMana);
        }

        [TestMethod]
        public void DrainMana_PercentageCalculated()
        {
            ivStats.DrainMana(50);

            Assert.AreEqual(0.5f, ivStats.ManaPercentage);
        }

        [TestMethod]
        public void RewardExperience_CurrentExp_Update()
        {
            ivStats.RewardExperience(10);

            Assert.AreEqual(10, ivStats.CurrentExp);
        }

        [TestMethod]
        public void RewardExperience_ExpToLevel_Updated()
        {
            ivStats.RewardExperience(10);

            Assert.AreEqual(90, ivStats.ExpToLevel);
        }

        [TestMethod]
        public void RewardExperience_ExpPercentage_Updated()
        {
            ivStats.RewardExperience(10);

            Assert.AreEqual(0.1f , ivStats.ExpPercentage);
        }

        [TestMethod]
        public void RewardExperience_CurrentExpOverMax_LevelIncreased()
        {
            ivStats.RewardExperience(110);

            Assert.AreEqual(10, ivStats.CurrentExp);
            Assert.AreEqual(2, ivStats.Level);
        }

        [TestMethod]
        public void RewardExperience_CurrentExpOverMax_MaxExpUpdated()
        {
            ivStats.RewardExperience(110);

            Assert.IsTrue(100 < ivStats.MaximumExp);
        }

        [TestMethod]
        public void RewardExperience_CurrentExpOverMaxManyTimes_LevelIncreasedManyTimes()
        {
            ivStats.RewardExperience(11540);

            Assert.IsTrue(2 < ivStats.Level);
        }

        [TestMethod]
        public void OperaterPlus_StatsAdded()
        {
            var newStats = ivStats + ivStats;

            Assert.AreEqual(200, newStats.MaximumHealth);
            Assert.AreEqual(200, newStats.Damage);
            Assert.AreEqual(200, newStats.MagicDamage);
        }

        [TestMethod]
        public void OperaterPlus_LevelNotIncreased()
        {
            var newStats = ivStats + ivStats;

            Assert.AreEqual(1, newStats.Level);
        }

        [TestMethod]
        public void OperaterPlus_HpPercent_StaysSame()
        {
            ivStats.DealDamage(50);

            var newStats = ivStats + new CStats(20);

            Assert.AreEqual(0.5f, newStats.HealthPercentage);
        }

        [TestMethod]
        public void OperaterPlus_MpPercent_StaysSame()
        {
            ivStats.DrainMana(50);

            var newStats = ivStats + new CStats(20);

            Assert.AreEqual(0.5f, newStats.ManaPercentage);
        }
    }
}
