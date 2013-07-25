using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class FractionalPart : Symbol
    {
        private FractionalPart(FullStop fullStop, DigitSequence digitSequence) 
            : base(fullStop, digitSequence)
        {
            DigitSequence = digitSequence;
        }

        private DigitSequence DigitSequence { get; set; }

        public double Evaluate()
        {
            var divisor = Math.Pow(10, DigitSequence.ConstituentSymbols.Count);
            return DigitSequence.Evaluate()/divisor;
        }

        public static FractionalPart Produce(IEnumerable<Symbol> symbols)
        {
            // fractional-part = full-stop digit-sequence

            if (!symbols.Any())
                return null;
            if (symbols.First() is FullStop)
            {
                IEnumerable<Symbol> s;
                var digitSequence = DigitSequence.Produce(symbols.Skip(1), out s);
                if (digitSequence == null || s.Any())
                    return null;
                return new FractionalPart(new FullStop(), digitSequence);
            }
            return null;
        }
    }
}