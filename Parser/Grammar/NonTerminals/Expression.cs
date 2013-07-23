using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;
using SimpleParser.Tools;

namespace SimpleParser.Grammar.NonTerminals
{
    public class Expression : Symbol
    {
        public Expression(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static Expression Produce(IEnumerable<Symbol> symbols)
        {
            // expression = *whitespace nospace-expression *whitespace

            var whiteSpaceBefore = symbols.TakeWhile(s => s is WhiteSpace).Count();
            var whiteSpaceAfter = symbols.Reverse().TakeWhile(s => s is WhiteSpace).Count();
            IEnumerable<Symbol> noSpaceSymbolList = symbols
                .Skip(whiteSpaceBefore)
                .SkipLast(whiteSpaceAfter)
                .ToList();
            var n = NospaceExpression.Produce(noSpaceSymbolList);
            if (n != null)
                return new Expression(
                    Enumerable.Repeat(new WhiteSpace(), whiteSpaceBefore),
                    n,
                    Enumerable.Repeat(new WhiteSpace(), whiteSpaceAfter));
            return null;
        }
    }
}