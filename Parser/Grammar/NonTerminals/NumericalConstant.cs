using System.Collections.Generic;
using System.Linq;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NumericalConstant : Symbol
    {
        private NumericalConstant(SignificandPart significandPart)
            : base(significandPart)
        {
            SignificandPart = significandPart;
        }

        private NumericalConstant(NegSign negSign, SignificandPart significandPart) 
            : base(negSign, significandPart)
        {
            NegSign = negSign;
            SignificandPart = significandPart;
        }

        private NegSign NegSign { get; set; }
        private SignificandPart SignificandPart { get; set; }

        public double Evaluate()
        {
            var sign = (NegSign != null)
                           ? -1
                           : 1;
            return sign*SignificandPart.Evaluate();
        }

        public static NumericalConstant Produce(IEnumerable<Symbol> symbols)
        {
            // numerical-constant = [neg-sign] significand-part

            var significandPart = SignificandPart.Produce(symbols);
            if (significandPart != null)
                return new NumericalConstant(significandPart);
            var negSign = NegSign.Produce(symbols.First());
            if (negSign != null)
            {
                var significandPart2 = SignificandPart.Produce(symbols.Skip(1));
                if (significandPart2 != null)
                    return new NumericalConstant(negSign, significandPart2);
            }
            return null;
        }
    }
}