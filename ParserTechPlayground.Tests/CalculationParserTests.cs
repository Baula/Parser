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
            Assert.AreEqual("two", addNode.Right.As<Value>().Identifier.Name);
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
            Assert.AreEqual(456, addNode.Right.As<Value>().Number.Value);
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
            Assert.AreEqual("two", subNode.Right.As<Value>().Identifier.Name);
            Assert.IsTrue(addNode.Operator.IsPlusNotMinus);
            Assert.AreEqual("three", addNode.Right.As<Value>().Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsHalfAddition_ThrowsProperException()
        {
            new Action(
                () => _parser.Parse("left=one+")
                )
                .ShouldThrow<ParseException>()
                .WithMessage("Expected end of file", ComparisonMode.Substring);
        }

        [TestMethod]
        public void ExpressionIsMultiplication()
        {
            var result = _parser.Parse("left=this*that");

            Assert.IsNotNull(result);
            Assert.AreEqual("left", result.Assignee.Name);
            var mulNode = result.Assigner.As<MulDiv>();
            Assert.AreEqual("this", mulNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("that", mulNode.Right.Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsHalfMultiplication_ThrowsProperException()
        {
            new Action(
                () => _parser.Parse("left=one*")
                )
                .ShouldThrow<ParseException>()
                .WithMessage("Expected end of file", ComparisonMode.Substring);
        }

        [TestMethod]
        public void ExpressionIsMixedAddSubMulDiv()
        {
            var result = _parser.Parse("left=one+two/three-four+five*six");

            Assert.IsNotNull(result);
            Assert.AreEqual("left", result.Assignee.Name);

            var addNode2 = result.Assigner.As<AddSub>();
            Assert.IsTrue(addNode2.Operator.IsPlusNotMinus);

            var subNode = addNode2.Left.As<AddSub>();
            Assert.IsFalse(subNode.Operator.IsPlusNotMinus);
            Assert.AreEqual("four", subNode.Right.As<Value>().Identifier.Name);

            var mulNode = addNode2.Right.As<MulDiv>();
            Assert.IsTrue(mulNode.Operator.IsTimes);
            Assert.AreEqual("five", mulNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("six", mulNode.Right.As<Value>().Identifier.Name);

            var addNode = subNode.Left.As<AddSub>();
            Assert.IsTrue(addNode.Operator.IsPlusNotMinus);
            Assert.AreEqual("one", addNode.Left.As<Value>().Identifier.Name);

            var divNode = addNode.Right.As<MulDiv>();
            Assert.IsTrue(divNode.Operator.IsDividedBy);
            Assert.AreEqual("two", divNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("three", divNode.Right.As<Value>().Identifier.Name);
        }
    }
}
