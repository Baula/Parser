using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;
using SimpleParser.Tools;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NospaceExpression : Symbol
    {
        private static readonly Dictionary<string, int> OperatorPrecedence = new Dictionary<string, int>
            {
                {"^", 3},
                {"*", 2},
                {"/", 2},
                {"+", 1},
                {"-", 1},
            };

        private NospaceExpression(NumericalConstant numericalConstant)
            : base(numericalConstant)
        {
            NumericalConstant = numericalConstant;
        }

        private NospaceExpression(Expression expressionLeft, InfixOperator infixOperator, Expression expressionRight)
            : base(expressionLeft, infixOperator, expressionRight)
        {
            ExpressionLeft = expressionLeft;
            InfixOperator = infixOperator;
            ExpressionRight = expressionRight;
        }

        private NospaceExpression(OpenParenthesis openParenthesis, Expression expression, CloseParenthesis closeParenthesis)
            : base(openParenthesis, expression, closeParenthesis)
        {
            Expression = expression;
        }

        private NospaceExpression(PrefixOperator prefixOperator, Expression expression) 
            : base(prefixOperator, expression)
        {
            PrefixOperator = prefixOperator;
            Expression = expression;
        }

        private PrefixOperator PrefixOperator { get; set; }
        private Expression Expression { get; set; }

        private NumericalConstant NumericalConstant { get; set; }
        private Expression ExpressionLeft { get; set; }
        private InfixOperator InfixOperator { get; set; }
        private Expression ExpressionRight { get; set; }

        public double Evaluate()
        {
            if (NumericalConstant != null) 
                return NumericalConstant.Evaluate();
            if (InfixOperator != null)
                return InfixOperator.Evaluate(ExpressionLeft.Evaluate(), ExpressionRight.Evaluate());
            if (PrefixOperator == null) 
                return Expression.Evaluate();
            return PrefixOperator.Evaluate(Expression.Evaluate());
        }

        public static NospaceExpression Produce(IEnumerable<Symbol> symbols)
        {
            // nospace-expression = open-parenthesis expression close-parenthesis
            //         / numerical-constant
            //         / prefix-operator expression
            //         / expression infix-operator expression

            if (!symbols.Any())
                return null;

            if (symbols.First() is OpenParenthesis && symbols.Last() is CloseParenthesis)
            {
                var expression = Expression.Produce(symbols.Skip(1).SkipLast(1));
                if (expression != null)
                    return new NospaceExpression(new OpenParenthesis(), expression, new CloseParenthesis());
            }

            // expression, infix-operator, expression
            var z = symbols.Rollup(0, (t, d) =>
                {
                    if (t is OpenParenthesis)
                        return d + 1;
                    if (t is CloseParenthesis)
                        return d - 1;
                    return d;
                });
            var symbolsWithIndex = symbols.Select((s, i) => new
                {
                    Symbol = s,
                    Index = i,
                });
            var z2 = symbolsWithIndex.Zip(z, (v1, v2) => new
                {
                    SymbolWithIndex = v1,
                    Depth = v2,
                });
            var operatorList = z2
                .Where(x => x.Depth == 0 &&
                            x.SymbolWithIndex.Index != 0 &&
                            InfixOperator.Produce(x.SymbolWithIndex.Symbol) != null)
                .ToList();
            if (operatorList.Any())
            {
                var minPrecedence = operatorList
                    .Select(o2 => OperatorPrecedence[o2.SymbolWithIndex.Symbol.ToString()]).Min();
                var op = operatorList
                    .Last(o2 => OperatorPrecedence[o2.SymbolWithIndex.Symbol.ToString()] == minPrecedence);
                if (op != null)
                {
                    var expressionTokenList1 = symbols.TakeWhile(t => t != op.SymbolWithIndex.Symbol);
                    var expressionLeft = Expression.Produce(expressionTokenList1);
                    if (expressionLeft == null)
                        throw new ParserException("Invalid expression");
                    var expressionTokenList2 = symbols
                        .SkipWhile(t => t != op.SymbolWithIndex.Symbol).Skip(1);
                    var expressionRight = Expression.Produce(expressionTokenList2);
                    if (expressionRight == null)
                        throw new ParserException("Invalid expression");
                    var infixOperator = new InfixOperator(op.SymbolWithIndex.Symbol);
                    return new NospaceExpression(expressionLeft, infixOperator, expressionRight);
                }
            }

            var numericalConstant = NumericalConstant.Produce(symbols);
            if (numericalConstant != null)
                return new NospaceExpression(numericalConstant);

            var prefixOperator = PrefixOperator.Produce(symbols.FirstOrDefault());
            if (prefixOperator != null)
            {
                var expression = Expression.Produce(symbols.Skip(1));
                if (expression != null)
                    return new NospaceExpression(prefixOperator, expression);
            }

            return null;
        }
    }
}