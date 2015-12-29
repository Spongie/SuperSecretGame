using Assets.Scripts.Character;
using Assets.Scripts.Character.Stats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTests.Buffs;

namespace UnitTests.Character
{
    [TestClass]
    public class PlayerControllerTests
    {
        private PlayerController ivController;

        [TestInitialize]
        public void Initialize()
        {
            var stats = new CStats()
            {
                Damage = 20,
                Defense = 10
            };

            stats.Resources.BaseHealth = 100;
            stats.Resources.CurrentHealth = 100;

            ivController = new PlayerController(new DummyBuffContainer(), stats, null);
        }

        [TestCleanup]
        public void CleanUp()
        {
            ivController = null;
        }

        [TestMethod]
        public void GetTrueStats_AddsStatsFromBuff()
        {
            var result = ivController.GetTrueStats();

            Assert.AreEqual(30, result.Damage);
            Assert.AreEqual(100, result.Resources.CurrentHealth);
        }
    }
}
