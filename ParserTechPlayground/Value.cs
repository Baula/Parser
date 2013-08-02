namespace ParserTechPlayground
{
    public class Value : IToken
    {
        private readonly Identifier _identifier;
        private readonly Number _number;

        public Value(Identifier identifier)
        {
            _identifier = identifier;
        }

        public Value(Number number)
        {
            _number = number;
        }

        public Identifier Identifier { get { return _identifier; } }
        public Number Number { get { return _number; } }

        // Value        : Identifier | Number
        internal static Value Produce(TokenBuffer tokens)
        {
            var identifier = Identifier.Produce(tokens);
            if (identifier != null)
                return new Value(identifier);

            var number = Number.Produce(tokens);
            if (number != null)
                return new Value(number);

            return null;
        }
    }
}
