using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ParserTechPlayground.Tests
{
    [TestClass]
    public class EvaluationTests
    {
        private CalculationParser _parser;

        [TestInitialize]
        public void SetupParser()
        {
            _parser = new CalculationParser();
        }

        [TestMethod]
        public void MixedExpression()
        {
            var parseResult = _parser.Parse(@"result=1+2-3*4/2--2^-3^2+27\3");

            var actual = parseResult.Assigner.Evaluate();
            // 1+2-6+512+3
            Assert.AreEqual(512, actual);
        }

        [TestMethod]
        public void MultiLineCalculationInAnyOrder()
        {
            _parser.Parse("one=1");
            _parser.Parse("twelve=10*one+two");
            _parser.Parse("two=2");

            Assert.AreEqual(12, Symbols.Get("twelve").Evaluate());
        }
    }
}
