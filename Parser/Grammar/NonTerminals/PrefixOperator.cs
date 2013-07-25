using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class PrefixOperator : Symbol
    {
        private PrefixOperator(object symbol) 
            : base(symbol)
        {
        }

        public double Evaluate(double rhs)
        {
            var operatorSymbol = ConstituentSymbols[0];
            return operatorSymbol is Minus
                       ? -rhs
                       : rhs;
        }

        public static PrefixOperator Produce(Symbol symbol)
        {
            // prefix-operator = plus / minus

            if (symbol is Plus || symbol is Minus)
                return new PrefixOperator(symbol);
            return null;
        }
    }
}