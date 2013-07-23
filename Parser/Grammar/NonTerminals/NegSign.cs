using System;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NegSign : Symbol
    {
        public NegSign(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static NegSign Produce(Symbol symbol)
        {
            // neg-sign = minus

            if (symbol is Minus)
                return new NegSign(symbol);
            return null;
        }
    }
}