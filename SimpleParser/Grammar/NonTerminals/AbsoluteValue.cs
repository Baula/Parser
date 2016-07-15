using System.Collections.Generic;
using System.Linq;

namespace SimpleParser.Grammar.NonTerminals
{
    public class AbsoluteValue : Symbol
    {
        private readonly double _value;

        private AbsoluteValue(WholeNumberPart wholeNumberPart)
            : base(wholeNumberPart)
        {
            _value = wholeNumberPart.Evaluate();
        }

        private AbsoluteValue(FractionalPart fractionalPart)
            : base(fractionalPart)
        {
            _value = fractionalPart.Value;
        }

        private AbsoluteValue(WholeNumberPart wholeNumberPart, FractionalPart fractionalPart) 
            : base(wholeNumberPart, fractionalPart)
        {
            _value = wholeNumberPart.Evaluate() +
                     fractionalPart.Value;
        }

        public double Value
        {
            get { return _value; }
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