using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;
using SimpleParser.Tools;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NospaceExpression : Symbol
    {
        public static Dictionary<string, int> OperatorPrecedence = new Dictionary<string, int>
            {
                {"^", 3},
                {"*", 2},
                {"/", 2},
                {"+", 1},
                {"-", 1},
            };

        public NospaceExpression(params Object[] symbols) 
            : base(symbols)
        {
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
                var e = Expression.Produce(symbols.Skip(1).SkipLast(1));
                if (e != null)
                    return new NospaceExpression(new OpenParenthesis(), e, new CloseParenthesis());
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
                    var e1 = Expression.Produce(expressionTokenList1);
                    if (e1 == null)
                        throw new ParserException("Invalid expression");
                    var expressionTokenList2 = symbols
                        .SkipWhile(t => t != op.SymbolWithIndex.Symbol).Skip(1);
                    var e2 = Expression.Produce(expressionTokenList2);
                    if (e2 == null)
                        throw new ParserException("Invalid expression");
                    var io = new InfixOperator(op.SymbolWithIndex.Symbol);
                    return new NospaceExpression(e1, io, e2);
                }
            }

            var n = NumericalConstant.Produce(symbols);
            if (n != null)
                return new NospaceExpression(n);

            var p = PrefixOperator.Produce(symbols.FirstOrDefault());
            if (p != null)
            {
                var e = Expression.Produce(symbols.Skip(1));
                if (e != null)
                    return new NospaceExpression(p, e);
            }

            return null;
        }
    }
}