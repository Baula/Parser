using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class NegSign : Symbol
    {
        private NegSign(Minus minus) 
            : base(minus)
        {
        }

        public static NegSign Produce(Symbol symbol)
        {
            // neg-sign = minus

            var minus = symbol as Minus;
            return minus == null
                       ? null
                       : new NegSign(minus);
        }
    }
}