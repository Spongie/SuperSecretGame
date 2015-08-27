using Assets.Scripts.Buffs;
using Assets.Scripts.Character.Stat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Buffs
{
    [TestClass]
    public class BuffTests
    {
        private Buff ivBuff;

        [TestInitialize]
        public void Initialize()
        {
            ivBuff = new Buff(new CStats());
        }

        [TestCleanup]
        public void CleanUp()
        {
            ivBuff = null;
        }

        [TestMethod]
        public void NewBuff_Duration_10()
        {
            Assert.AreEqual(10f, ivBuff.Duration);
        }

        [TestMethod]
        public void NewBuff_InputDuration_Duration()
        {
            ivBuff = new Buff(new CStats(), 22);

            Assert.AreEqual(22, ivBuff.Duration);
        }

        [TestMethod]
        public void Expired_TimerNotDone_False()
        {
            Assert.IsFalse(ivBuff.Expired);
        }

        [TestMethod]
        public void Expired_TimerDone_True()
        {
            ivBuff.Update(2000);
            Assert.IsTrue(ivBuff.Expired);
        }
    }
}
