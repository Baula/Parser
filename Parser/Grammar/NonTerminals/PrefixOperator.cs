using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class PrefixOperator : Symbol
    {
        private PrefixOperator(Symbol operatorSymbol) 
            : base(operatorSymbol)
        {
            OperatorSymbol = operatorSymbol;
        }

        private Symbol OperatorSymbol { get; set; }

        public double Evaluate(double rhs)
        {
            return OperatorSymbol is Minus
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