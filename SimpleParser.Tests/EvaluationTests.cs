using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleParser.Tests
{
    [TestClass]
    public class EvaluationTests
    {
        [TestMethod]
        public void Evaluate_PositiveWholeNumber()
        {
            AssertEvaluate("123", 123);
        }

        [TestMethod]
        public void Evaluate_NegativeWholelNumber()
        {
            AssertEvaluate("-123", -123);
        }

        [TestMethod]
        public void Evaluate_FractionalNumberWithoutWholePart()
        {
            AssertEvaluate(".123", .123);
        }

        [TestMethod]
        public void Evaluate_FractionalNumberWithWholePart()
        {
            AssertEvaluate("12.34", 12.34);
        }

        [TestMethod]
        public void Evaluate_PrefixPlus()
        {
            AssertEvaluate("+12.34", 12.34);
        }

        [TestMethod]
        public void Evaluate_PrefixMinus()
        {
            AssertEvaluate("-(1+2)", -3);
        }

        [TestMethod]
        public void Evaluate_Addition()
        {
            AssertEvaluate("12+34", 46);
        }

        [TestMethod]
        public void Evaluate_Subtraction()
        {
            AssertEvaluate("12-34", -22);
        }

        [TestMethod]
        public void Evaluate_Multiplication()
        {
            AssertEvaluate("12*34", 408);
        }

        [TestMethod]
        public void Evaluate_Division()
        {
            AssertEvaluate("123/246", 0.5);
        }

        [TestMethod]
        public void Evaluate_ToThePower()
        {
            AssertEvaluate("2^8", 256);
        }

        [TestMethod]
        public void Evaluate_ExpressionInParenthesis()
        {
            AssertEvaluate("(1+2)", 3);
        }

        private static void AssertEvaluate(string formulaAsString, double expectedEvaluationResult)
        {
            var formula = Parser.ParseFormula(formulaAsString);

            // Act
            var actual = formula.Evaluate();

            Assert.AreEqual(expectedEvaluationResult, actual);
        }
    }
}
