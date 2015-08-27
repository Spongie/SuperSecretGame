using Assets.Scripts.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Utility
{
    [TestClass]
    public class ButtonLockTests
    {
        [TestMethod]
        public void AddLock_LockAdded()
        {
            ButtonLock.Instance.AddLock("A");

            Assert.IsTrue(ButtonLock.Instance.IsButtonLocked("A"));
        }

        [TestMethod]
        public void AddLock_AlreadyLocked_StillLocked()
        {
            ButtonLock.Instance.AddLock("A");
            ButtonLock.Instance.AddLock("A");

            Assert.IsTrue(ButtonLock.Instance.IsButtonLocked("A"));
        }

        [TestMethod]
        public void ClearLock_NoLock_Nothing()
        {
            ButtonLock.Instance.ClearLock("asd");

            Assert.IsFalse(ButtonLock.Instance.IsButtonLocked("asd"));
        }

        [TestMethod]
        public void ClearLock_LockExists_Cleared()
        {
            ButtonLock.Instance.AddLock("asd");

            Assert.IsTrue(ButtonLock.Instance.IsButtonLocked("asd"));

            ButtonLock.Instance.ClearLock("asd");

            Assert.IsFalse(ButtonLock.Instance.IsButtonLocked("asd"));
        }
    }
}