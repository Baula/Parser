using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class DigitSequence : Symbol
    {
        private readonly int _digitCount;
        private readonly double _value;

        private DigitSequence(IEnumerable<DecimalDigit> decimalDigits)
            : base(decimalDigits)
        {
            _digitCount = decimalDigits.Count();
            _value = decimalDigits.Aggregate<DecimalDigit, double>(0, (sum, digit) => 10*sum + digit.Value);
        }

        public int DigitCount
        {
            get { return _digitCount; }
        }

        public double Value
        {
            get { return _value; }
        }

        public static DigitSequence Produce(IEnumerable<Symbol> symbols,
                                            out IEnumerable<Symbol> symbolsToProcess)
        {
            // digit-sequence = 1*decimal-digit

            var digits = symbols.TakeWhile(s => s is DecimalDigit)
                                .Cast<DecimalDigit>();
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