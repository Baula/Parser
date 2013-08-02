using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssignmentParserPlayground
{
    [TestClass]
    public class AssignmentParserTests
    {
        private AssignmentParser _parser;

        [TestInitialize]
        public void SetupParser()
        {
            _parser = new AssignmentParser();
        }

        [TestMethod]
        public void Playground()
        {
            var result = _parser.Parse("left=right");

            Assert.AreEqual("left", result.Assignee.Name);
            Assert.AreEqual("right", result.Assigner.Name);
        }
    }
}
