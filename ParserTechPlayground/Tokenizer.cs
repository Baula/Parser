namespace ParserTechPlayground
{
    class Tokenizer
    {
        public TokenBuffer Tokenize(string input)
        {
            var buffer = new TokenBuffer();
            foreach (var c in input)
            {
                var token = GetToken(c);
                buffer.Add(token);
            }
            buffer.Add(new EOF());
            return buffer;
        }

        private IToken GetToken(char c)
        {
            if (char.IsLetter(c))
                return new Character(c);
            if (char.IsNumber(c))
                return new Digit(c);
            if (c == '=')
                return new AssignmentOperator();
            if (c == '+')
                return new AdditionOperator();
            if (c == '-')
                return new SubtractionOperator();
            throw new ParseException(string.Format("Invalid character '{0}'.", c));
        }
    }
}
