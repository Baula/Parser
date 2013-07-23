using System;
using System.Collections.Generic;

namespace SimpleParser.Grammar.NonTerminals
{
    public class Formula : Symbol
    {
        public Formula(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static Formula Produce(IEnumerable<Symbol> symbols)
        {
            // formula = expression

            var e = Expression.Produce(symbols);
            return e == null ? null : new Formula(e);
        }
    }
}