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
            Assert.IsTrue(addNode.Operator.IsPlus);
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
            Assert.IsTrue(addNode.Operator.IsPlus);
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
            Assert.IsFalse(subNode.Operator.IsPlus);
            Assert.AreEqual("two", subNode.Right.As<Value>().Identifier.Name);
            Assert.IsTrue(addNode.Operator.IsPlus);
            Assert.AreEqual("three", addNode.Right.As<Value>().Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsJustAdditionOperator_ThrowsProperException()
        {
            new Action(
                () => _parser.Parse("left=+")
                )
                .ShouldThrow<ParseException>()
                .WithMessage("Expected expression for the right side of the assignment", ComparisonMode.Substring);
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
            Assert.AreEqual("that", mulNode.Right.As<Value>().Identifier.Name);
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
            Assert.IsTrue(addNode2.Operator.IsPlus);

            var subNode = addNode2.Left.As<AddSub>();
            Assert.IsFalse(subNode.Operator.IsPlus);
            Assert.AreEqual("four", subNode.Right.As<Value>().Identifier.Name);

            var mulNode = addNode2.Right.As<MulDiv>();
            Assert.IsTrue(mulNode.Operator.IsTimes);
            Assert.AreEqual("five", mulNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("six", mulNode.Right.As<Value>().Identifier.Name);

            var addNode = subNode.Left.As<AddSub>();
            Assert.IsTrue(addNode.Operator.IsPlus);
            Assert.AreEqual("one", addNode.Left.As<Value>().Identifier.Name);

            var divNode = addNode.Right.As<MulDiv>();
            Assert.IsTrue(divNode.Operator.IsDividedBy);
            Assert.AreEqual("two", divNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("three", divNode.Right.As<Value>().Identifier.Name);
        }

        [TestMethod]
        public void SupportForWhitespace()
        {
            _parser.Parse(" result\t= 2 * 3 ");

            Assert.AreEqual(6, Symbols.Get("result").Evaluate());
        }

        [TestMethod]
        public void ExpressionIsExponentiation()
        {
            var result = _parser.Parse("left=this^that");

            Assert.IsNotNull(result);
            Assert.AreEqual("left", result.Assignee.Name);
            var expNode = result.Assigner.As<ExpRoot>();
            Assert.IsTrue(expNode.Operator.IsExponentiation);
            Assert.AreEqual("this", expNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("that", expNode.Right.As<Value>().Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsHalfExponentiation_ThrowsProperException()
        {
            new Action(
                () => _parser.Parse("left=one^")
                )
                .ShouldThrow<ParseException>()
                .WithMessage("Expected expression for the right side of the assignment.", ComparisonMode.Substring);
        }

        [TestMethod]
        public void ExpressionIsMultipleExponentiation()
        {
            var result = _parser.Parse("left=this^that^another");

            Assert.IsNotNull(result);
            Assert.AreEqual("left", result.Assignee.Name);

            // mind: exponentiation is right associative
            var topExpNode = result.Assigner.As<ExpRoot>();
            Assert.IsTrue(topExpNode.Operator.IsExponentiation);
            Assert.AreEqual("this", topExpNode.Left.As<Value>().Identifier.Name);

            var subExpNode = topExpNode.Right.As<ExpRoot>();
            Assert.IsTrue(subExpNode.Operator.IsExponentiation);
            Assert.AreEqual("that", subExpNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("another", subExpNode.Right.As<Value>().Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsRooting()
        {
            var result = _parser.Parse(@"left=this\that");

            Assert.IsNotNull(result);
            Assert.AreEqual("left", result.Assignee.Name);
            var rootNode = result.Assigner.As<ExpRoot>();
            Assert.IsTrue(rootNode.Operator.IsRootExtraction);
            Assert.AreEqual("this", rootNode.Left.As<Value>().Identifier.Name);
            Assert.AreEqual("that", rootNode.Right.As<Value>().Identifier.Name);
        }

        [TestMethod]
        public void ExpressionIsHalfRooting_ThrowsProperException()
        {
            new Action(
                () => _parser.Parse("left=one//")
                )
                .ShouldThrow<ParseException>()
                .WithMessage("Expected end of file", ComparisonMode.Substring);
        }

        [TestMethod]
        public void UnaryMinus()
        {
            var result = _parser.Parse("aNumber = -123");

            Assert.AreEqual(-123, Symbols.Get("aNumber").Evaluate());
        }

        [TestMethod]
        public void NumberInParenthesis()
        {
            var result = _parser.Parse("aNumber = (123)");

            Assert.IsNotNull(result);
            Assert.AreEqual("aNumber", result.Assignee.Name);
            var number = result.Assigner.As<Value>().Number;
            Assert.AreEqual(123, number.Value);
        }

        [TestMethod]
        public void ParenthesisWithUnaryMinus()
        {
            var result = _parser.Parse("aNumber = -(-123)");

            Assert.IsNotNull(result);
            Assert.AreEqual("aNumber", result.Assignee.Name);
            var outerUnary = result.Assigner.As<Unary>();
            var innerUnary = outerUnary.Operand.As<Unary>();
            var number = innerUnary.Operand.As<Value>().Number;
            Assert.AreEqual(123, number.Value);
            Assert.AreEqual(123, Symbols.Get("aNumber").Evaluate());
        }

        [TestMethod]
        public void NegativeExponent()
        {
            var result = _parser.Parse("aNumber = 2^-3");

            Assert.AreEqual(0.125, Symbols.Get("aNumber").Evaluate());
        }

        [TestMethod]
        public void ExpressionUsingParenthesis()
        {
            var result = _parser.Parse("aNumber = -(1+2)*3 + (2^2)^-2");

            // -3*3 + 8^-2 = -9 + 1/16
            Assert.AreEqual(-8.9375, Symbols.Get("aNumber").Evaluate());
        }

        [TestMethod]
        public void ExpressionMissingClosingParenthesis_ThrowsProperException()
        {
            new Action(
                () => _parser.Parse("left = (justopen")
                )
                .ShouldThrow<ParseException>()
                .WithMessage("Missing closing parenthesis", ComparisonMode.Substring);
        }

        [TestMethod]
        public void NestedParenthesis()
        {
            var result = _parser.Parse("aNumber = 123 + ((1+2) ^ (3*4))");

            // 123 + (3 ^ 12)
            Assert.AreEqual(531564, Symbols.Get("aNumber").Evaluate());
        }
    }
}
