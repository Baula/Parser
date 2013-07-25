using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class DigitSequence : Symbol
    {
        private DigitSequence(IEnumerable<DecimalDigit> decimalDigits) 
            : base(decimalDigits)
        {
            DecimalDigits = decimalDigits;
        }

        private IEnumerable<DecimalDigit> DecimalDigits { get; set; }

        public double Evaluate()
        {
            return DecimalDigits.Aggregate<DecimalDigit, double>(0,
                                                                 (sum, digit) => 10*sum + digit.Evaluate());
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