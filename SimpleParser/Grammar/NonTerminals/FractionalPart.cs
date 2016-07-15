using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class FractionalPart : Symbol
    {
        private readonly double _value;

        private FractionalPart(FullStop fullStop, DigitSequence digitSequence)
            : base(fullStop, digitSequence)
        {
            var divisor = Math.Pow(10, digitSequence.DigitCount);
            _value = digitSequence.Value/divisor;
        }

        public double Value
        {
            get { return _value; }
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