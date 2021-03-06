﻿namespace ParserTechPlayground
{
    class Tokenizer
    {
        public TokenBuffer Tokenize(string input)
        {
            var buffer = new TokenBuffer();
            foreach (var c in input)
            {
                var token = GetToken(c);
                if (token != null)
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
            if (c == '*')
                return new MultiplicationOperator();
            if (c == '/')
                return new DivisionOperator();
            if (c == '^')
                return new ExponentiationOperator();
            if (c == '\\')
                return new RootExtractionOperator();
            if (c == '(')
                return new LeftParenthesis();
            if (c == ')')
                return new RightParenthesis();
            if (c == ' ' || c == '\t')
                return null;
            throw new ParseException(string.Format("Invalid character '{0}' (0x{1:x2}).", c, (int)c));
        }
    }
}
