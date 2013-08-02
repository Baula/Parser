using System;

namespace ParserTechPlayground
{
    // Expression   : Identifier
    public class Expression : IToken
    {
        private readonly Identifier _identifier;
        public Identifier Identifier { get { return _identifier; } }

        public Expression(Identifier identifier)
        {
            _identifier = identifier;
        }

        internal static Expression Produce(TokenBuffer tokens)
        {
            var identifier = Identifier.Produce(tokens);
            if (identifier == null)
                return null;

            return new Expression(identifier);
        }
    }
}
