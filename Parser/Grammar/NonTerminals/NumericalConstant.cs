using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NumericalConstant : Symbol
    {
        public NumericalConstant(params Object[] symbols) 
            : base(symbols)
        {
        }

        public static NumericalConstant Produce(IEnumerable<Symbol> symbols)
        {
            // numerical-constant = [neg-sign] significand-part

            var s = SignificandPart.Produce(symbols);
            if (s != null)
                return new NumericalConstant(s);
            var n = NegSign.Produce(symbols.First());
            if (n != null)
            {
                var s2 = SignificandPart.Produce(symbols.Skip(1));
                if (s2 != null)
                    return new NumericalConstant(n, s2);
            }
            return null;
        }
    }
}