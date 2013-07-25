using System.Collections.Generic;
using System.Linq;

namespace SimpleParser.Grammar.NonTerminals
{
    public class AbsoluteValue : Symbol
    {
        private AbsoluteValue(WholeNumberPart wholeNumberPart)
            : base(wholeNumberPart)
        {
            WholeNumberPart = wholeNumberPart;
        }

        private AbsoluteValue(FractionalPart fractionalPart)
            : base(fractionalPart)
        {
            FractionalPart = fractionalPart;
        }

        private AbsoluteValue(WholeNumberPart wholeNumberPart, FractionalPart fractionalPart) 
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

        public static AbsoluteValue Produce(IEnumerable<Symbol> symbols)
        {
            // absolute-value = whole-number-part [fractional-part] / fractional-part

            var fractionalPart = FractionalPart.Produce(symbols);
            if (fractionalPart != null)
                return new AbsoluteValue(fractionalPart);

            IEnumerable<Symbol> symbolsToProcess;
            var wholeNumberPart = WholeNumberPart.Produce(symbols, out symbolsToProcess);
            if (wholeNumberPart != null)
            {
                if (!symbolsToProcess.Any())
                    return new AbsoluteValue(wholeNumberPart);

                fractionalPart = FractionalPart.Produce(symbolsToProcess);
                if (fractionalPart != null)
                    return new AbsoluteValue(wholeNumberPart, fractionalPart);
            }
            return null;
        }
    }
}