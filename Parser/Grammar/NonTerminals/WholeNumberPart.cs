using System.Collections.Generic;

namespace SimpleParser.Grammar.NonTerminals
{
    public class WholeNumberPart : Symbol
    {
        private readonly double _value;

        private WholeNumberPart(DigitSequence digitSequence)
            :base(digitSequence)
        {
            _value = digitSequence.Value;
        }

        public double Evaluate()
        {
            return _value;
        }

        public static WholeNumberPart Produce(IEnumerable<Symbol> symbols,
                                              out IEnumerable<Symbol> symbolsToProcess)
        {
            // whole-number-part = digit-sequence

            IEnumerable<Symbol> s;
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