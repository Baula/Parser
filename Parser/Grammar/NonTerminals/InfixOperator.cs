using System;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser.Grammar.NonTerminals
{
    public class InfixOperator : Symbol
    {
        public InfixOperator(params Object[] symbols)
            : base(symbols)
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
    }
}