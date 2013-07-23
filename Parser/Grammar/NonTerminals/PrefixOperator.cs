using System;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class PrefixOperator : Symbol
    {
        public PrefixOperator(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static PrefixOperator Produce(Symbol symbol)
        {
            // prefix-operator = plus / minus

            if (symbol is Plus || symbol is Minus)
                return new PrefixOperator(symbol);
            return null;
        }
    }
}