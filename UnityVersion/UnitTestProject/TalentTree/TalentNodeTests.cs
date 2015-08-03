using Microsoft.VisualStudio.TestTools.UnitTesting;
using TalentTree;

namespace UnitTestProject.TalentTree
{
    [TestClass]
    public class TalentNodeTests
    {
        private TalentTreeHelper ivHelper;

        [TestInitialize]
        public void Init()
        {
            ivHelper = new TalentTreeHelper();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ivHelper = null;
        }

        [TestMethod]
        public void Upgrade_NotUpgraded_True()
        {
            var node = new TalentNode(null, 0, 0, 0);

            var result = node.Upgrade();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Upgrade_Upgraded_False()
        {
            var node = new TalentNode(null, 0, 0, 0);

            var result = node.Upgrade();
            Assert.IsTrue(result);

            result = node.Upgrade();
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetParent_NoParent_Null()
        {
            var node = new TalentNode(null, 0, 0, 0);

            var result = node.GetParent();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetParent_GotParent_Parent()
        {
            var parent = new TalentNode(null, 0, 0, 0);

            var result = node.GetParent();
            Assert.IsNull(result);
        }
    }
}
