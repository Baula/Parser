using System;
using System.Collections.Generic;

namespace SimpleParser.Grammar.NonTerminals
{
    public class WholeNumberPart : Symbol
    {
        public WholeNumberPart(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static WholeNumberPart Produce(IEnumerable<Symbol> symbols,
                                              out IEnumerable<Symbol> symbolsToProcess)
        {
            // whole-number-part = digit-sequence

            IEnumerable<Symbol> s = null;
            var d = DigitSequence.Produce(symbols, out s);
            if (d != null)
            {
                symbolsToProcess = s;
                return new WholeNumberPart(d);
            }
            symbolsToProcess = null;
            return null;
        }
    }
}