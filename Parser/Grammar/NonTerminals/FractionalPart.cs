using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class FractionalPart : Symbol
    {
        public FractionalPart(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static FractionalPart Produce(IEnumerable<Symbol> symbols)
        {
            // fractional-part = full-stop digit-sequence

            if (!symbols.Any())
                return null;
            if (symbols.First() is FullStop)
            {
                IEnumerable<Symbol> s;
                var d = DigitSequence.Produce(symbols.Skip(1), out s);
                if (d == null || s.Any())
                    return null;
                return new FractionalPart(new FullStop(), d);
            }
            return null;
        }
    }
}