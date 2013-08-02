using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParserTechPlayground.Tests
{
    [TestClass]
    public class CalculationParserTests
    {
        private CalculationParser _parser;

        [TestInitialize]
        public void SetupParser()
        {
            _parser = new CalculationParser();
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
