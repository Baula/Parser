using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;
using SimpleParser.Tools;

namespace SimpleParser.Grammar.NonTerminals
{
    public class Expression : Symbol
    {
        private Expression(IEnumerable<WhiteSpace> leadingWhiteSpaces, 
                           NospaceExpression nospaceExpression, 
                           IEnumerable<WhiteSpace> trailingWhiteSpaces) 
            : base(leadingWhiteSpaces, nospaceExpression, trailingWhiteSpaces)
        {
            NospaceExpression = nospaceExpression;
        }

        private NospaceExpression NospaceExpression { get; set; }

        public double Evaluate()
        {
            return NospaceExpression.Evaluate();
        }

        public static Expression Produce(IEnumerable<Symbol> symbols)
        {
            // expression = *whitespace nospace-expression *whitespace

            var whiteSpaceBefore = symbols.TakeWhile(s => s is WhiteSpace)
                                          .Count();
            var whiteSpaceAfter = symbols.Reverse()
                                         .TakeWhile(s => s is WhiteSpace)
                                         .Count();
            var noSpaceSymbolList = symbols.Skip(whiteSpaceBefore)
                                           .SkipLast(whiteSpaceAfter)
                                           .ToList();  // ToList is vital here!
            var nospaceExpression = NospaceExpression.Produce(noSpaceSymbolList);
            return nospaceExpression == null
                       ? null
                       : new Expression(
                             Enumerable.Repeat(new WhiteSpace(), whiteSpaceBefore),
                             nospaceExpression,
                             Enumerable.Repeat(new WhiteSpace(), whiteSpaceAfter));
        }
    }
}