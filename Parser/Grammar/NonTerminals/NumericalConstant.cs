using System.Collections.Generic;
using System.Linq;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NumericalConstant : Symbol
    {
        private readonly double _value;

        private NumericalConstant(AbsoluteValue absoluteValue)
            : base(absoluteValue)
        {
            _value = absoluteValue.Value;
        }

        private NumericalConstant(NegSign negSign, AbsoluteValue absoluteValue) 
            : base(negSign, absoluteValue)
        {
            _value = -absoluteValue.Value;
        }

        public double Value
        {
            get { return _value; }
        }

        public static NumericalConstant Produce(IEnumerable<Symbol> symbols)
        {
            // numerical-constant = [neg-sign] absolute-value

            var absoluteValue = AbsoluteValue.Produce(symbols);
            if (absoluteValue != null)
                return new NumericalConstant(absoluteValue);

            var negSign = NegSign.Produce(symbols.First());
            if (negSign != null)
            {
                var absoluteValue2 = AbsoluteValue.Produce(symbols.Skip(1));
                if (absoluteValue2 != null)
                    return new NumericalConstant(negSign, absoluteValue2);
            }

            return null;
        }
    }
}