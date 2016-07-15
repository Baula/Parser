using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreLib.Tests
{
    [TestClass]
    public class CoreTestClass
    {
        [TestMethod]
        public void CoreTestMethod()
        {
            var coreClass = new CoreLibClass();

            coreClass.Name = "Hallo";

            Assert.AreEqual("Hallo", coreClass.Name);
        }
    }
}
