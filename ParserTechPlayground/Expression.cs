using System;

namespace ParserTechPlayground
{
    public class Expression : IToken
    {
        private readonly Value _value;

        public Expression(Value value)
        {
            _value = value;
        }

        public Value Value { get { return _value; } }

        // Expression   : Value
        internal static Expression Produce(TokenBuffer tokens)
        {
            var value = Value.Produce(tokens);
            if (value != null)
                return new Expression(value);

            return null;
        }
    }
}
