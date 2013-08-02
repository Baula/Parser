using System;

namespace ParserTechPlayground
{
    public class Expression : IToken
    {
        private readonly Identifier _identifier;
        private readonly Number _number;

        public Expression(Identifier identifier)
        {
            _identifier = identifier;
        }

        public Expression(Number number)
        {
            _number = number;
        }

        public Identifier Identifier { get { return _identifier; } }
        public Number Number { get { return _number; } }

        // Expression   : Identifier | Number
        internal static Expression Produce(TokenBuffer tokens)
        {
            var identifier = Identifier.Produce(tokens);
            if (identifier != null)
                return new Expression(identifier);

            var number = Number.Produce(tokens);
            if (number != null)
                return new Expression(number);

            return null;
        }
    }
}
