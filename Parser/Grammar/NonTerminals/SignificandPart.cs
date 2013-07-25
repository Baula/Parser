using System.Collections.Generic;
using System.Linq;

namespace SimpleParser.Grammar.NonTerminals
{
    public class SignificandPart : Symbol
    {
        private SignificandPart(WholeNumberPart wholeNumberPart)
            : base(wholeNumberPart)
        {
            WholeNumberPart = wholeNumberPart;
        }

        private SignificandPart(FractionalPart fractionalPart)
            : base(fractionalPart)
        {
            FractionalPart = fractionalPart;
        }

        private SignificandPart(WholeNumberPart wholeNumberPart, FractionalPart fractionalPart) 
            : base(wholeNumberPart, fractionalPart)
        {
            WholeNumberPart = wholeNumberPart;
            FractionalPart = fractionalPart;
        }

        private FractionalPart FractionalPart { get; set; }
        private WholeNumberPart WholeNumberPart { get; set; }

        public double Evaluate()
        {
            double d = 0;
            if (WholeNumberPart != null)
            {
                d = WholeNumberPart.Evaluate();
            }
            if (FractionalPart != null)
            {
                d += FractionalPart.Evaluate();
            }
            return d;
        }

        public static SignificandPart Produce(IEnumerable<Symbol> symbols)
        {
            // significand-part = whole-number-part [fractional-part] / fractional-part

            var fractionalPart = FractionalPart.Produce(symbols);
            if (fractionalPart != null)
                return new SignificandPart(fractionalPart);
            IEnumerable<Symbol> symbolsToProcess;
            var wholeNumberPart = WholeNumberPart.Produce(symbols, out symbolsToProcess);
            if (wholeNumberPart != null)
            {
                if (!symbolsToProcess.Any())
                    return new SignificandPart(wholeNumberPart);
                fractionalPart = FractionalPart.Produce(symbolsToProcess);
                if (fractionalPart != null)
                    return new SignificandPart(wholeNumberPart, fractionalPart);
            }
            return null;
        }
    }
}