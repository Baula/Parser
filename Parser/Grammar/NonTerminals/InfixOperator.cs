using System;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class InfixOperator : Symbol
    {
        public InfixOperator(object operatorSymbol)
            : base(operatorSymbol)
        {
        }

        public static InfixOperator Produce(Symbol symbol)
        {
            // infix-operator = caret / asterisk / forward-slash / plus / minus

            if (symbol is Plus ||
                symbol is Minus ||
                symbol is Asterisk ||
                symbol is ForwardSlash ||
                symbol is Caret)
                return new InfixOperator(symbol);
            return null;
        }

        internal double Evaluate(double lhs, double rhs)
        {
            var operatorSymbol = ConstituentSymbols[0];
            if (operatorSymbol is Plus)
                return lhs + rhs;
            if (operatorSymbol is Minus)
                return lhs - rhs;
            if (operatorSymbol is Asterisk)
                return lhs * rhs;
            if (operatorSymbol is ForwardSlash)
                return lhs / rhs;
            if (operatorSymbol is Caret)
                return Math.Pow(lhs, rhs);
            throw new InvalidOperationException(string.Format("Invalid infix operator \"{0}\".", operatorSymbol));
        }
    }
}