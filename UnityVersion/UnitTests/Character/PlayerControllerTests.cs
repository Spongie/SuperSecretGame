using Assets.Scripts.Character;
using Assets.Scripts.Character.Stat;
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
                MaximumHealth = 100,
                CurrentHealth = 100,
                Damage = 20,
                Defense = 10
            };

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
            Assert.AreEqual(100, result.CurrentHealth);
        }
    }
}
