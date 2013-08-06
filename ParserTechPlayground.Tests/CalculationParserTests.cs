using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            Assert.AreEqual("right", result.Assigner.As<Value>().Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsNumber()
        {
            var result = _parser.Parse("left=123");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            Assert.AreEqual(123, result.Assigner.As<Value>().Number.Value);
        }

        [TestMethod]
        public void ExpressionIsAdditionOfIdentifiers()
        {
            var result = _parser.Parse("left=one+two");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            var addNode = result.Assigner.As<AddSub>();
            Assert.AreEqual("one", addNode.Left.As<Value>().Identifier.Name);
            Assert.IsTrue(addNode.Operator.IsPlusNotMinus);
            Assert.AreEqual("two", addNode.Right.Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsAdditionOfNumbers()
        {
            var result = _parser.Parse("left=123+456");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            var addNode = result.Assigner.As<AddSub>();
            Assert.AreEqual(123, addNode.Left.As<Value>().Number.Value);
            Assert.IsTrue(addNode.Operator.IsPlusNotMinus);
            Assert.AreEqual(456, addNode.Right.Number.Value);
        }

        [TestMethod]
        public void ExpressionIsAdditionOfMoreThanTwoIdentifiers()
        {
            var result = _parser.Parse("left=one-two+three");

            Assert.IsNotNull(result, "The result should not be null.");
            Assert.AreEqual("left", result.Assignee.Name);
            var addNode = result.Assigner.As<AddSub>();
            var subNode = addNode.Left.As<AddSub>();
            Assert.AreEqual("one", subNode.Left.As<Value>().Identifier.Name);
            Assert.IsFalse(subNode.Operator.IsPlusNotMinus);
            Assert.AreEqual("two", subNode.Right.Identifier.Name);
            Assert.IsTrue(addNode.Operator.IsPlusNotMinus);
            Assert.AreEqual("three", addNode.Right.Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsHalfAddition()
        {
            new Action(
                () => _parser.Parse("left=one+")
                )
                .ShouldThrow<ParseException>()
                .WithMessage("Expected end of file", ComparisonMode.Substring);
        }
    }
}
