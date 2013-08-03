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
        public void ExpressionIsIdentifier()
        {
            var result = _parser.Parse("left=right");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            Assert.AreEqual("right", result.Assigner.Value.Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsNumber()
        {
            var result = _parser.Parse("left=123");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            Assert.AreEqual(123, result.Assigner.Value.Number.Value);
        }

        [TestMethod]
        public void ExpressionIsAdditionOfIdentifiers()
        {
            var result = _parser.Parse("left=one+two");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            Assert.AreEqual("one", result.Assigner.AddSub.Left.Identifier.Name);
            Assert.IsTrue(result.Assigner.AddSub.Operator.IsPlusNotMinus);
            Assert.AreEqual("two", result.Assigner.AddSub.Right.Value.Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsAdditionOfNumbers()
        {
            var result = _parser.Parse("left=123+456");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            Assert.AreEqual(123, result.Assigner.AddSub.Left.Number.Value);
            Assert.IsTrue(result.Assigner.AddSub.Operator.IsPlusNotMinus);
            Assert.AreEqual(456, result.Assigner.AddSub.Right.Value.Number.Value);
        }

        [TestMethod]
        public void ExpressionIsAdditionOfMoreThanTwoIdentifiers()
        {
            var result = _parser.Parse("left=one+two-three");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            Assert.AreEqual("one", result.Assigner.AddSub.Left.Identifier.Name);
            Assert.IsTrue(result.Assigner.AddSub.Operator.IsPlusNotMinus);
            Assert.AreEqual("two", result.Assigner.AddSub.Right.AddSub.Left.Identifier.Name);
            Assert.IsFalse(result.Assigner.AddSub.Right.AddSub.Operator.IsPlusNotMinus);
            Assert.AreEqual("three", result.Assigner.AddSub.Right.AddSub.Right.Value.Identifier.Name);
        }
    }
}
