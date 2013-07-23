using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class DigitSequence : Symbol
    {
        public DigitSequence(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static DigitSequence Produce(IEnumerable<Symbol> symbols,
                                            out IEnumerable<Symbol> symbolsToProcess)
        {
            // digit-sequence = 1*decimal-digit

            var digits = symbols.TakeWhile(s => s is DecimalDigit);
            if (digits.Any())
            {
                symbolsToProcess = symbols.Skip(digits.Count());
                return new DigitSequence(digits);
            }
            symbolsToProcess = null;
            return null;
        }
    }
}