using System.Collections.Generic;
using System.Linq;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NumericalConstant : Symbol
    {
        private NumericalConstant(AbsoluteValue absoluteValue)
            : base(absoluteValue)
        {
            AbsoluteValue = absoluteValue;
        }

        private NumericalConstant(NegSign negSign, AbsoluteValue absoluteValue) 
            : base(negSign, absoluteValue)
        {
            NegSign = negSign;
            AbsoluteValue = absoluteValue;
        }

        private NegSign NegSign { get; set; }
        private AbsoluteValue AbsoluteValue { get; set; }

        public double Evaluate()
        {
            var sign = (NegSign != null)
                           ? -1
                           : 1;
            return sign*AbsoluteValue.Evaluate();
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