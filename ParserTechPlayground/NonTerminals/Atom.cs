namespace ParserTechPlayground
{
    public class Atom : INode
    {
        // Atom         : Value | LParen Expression RParen
        internal static INode Produce(TokenBuffer tokens)
        {
            var value = Value.Produce(tokens);
            if (value != null)
                return value;

            // HACK
            if (tokens.Current is LeftParenthesis)
                tokens.SavePosition();

            var lParen = tokens.GetTerminal<LeftParenthesis>();
            if (lParen == null)
                return null;

            var expression = Expression.Produce(tokens);
            if (expression == null)
            {
                tokens.RestorePosition();
                return null;
            }

            var rParen = tokens.GetTerminal<RightParenthesis>();
            if (rParen == null)
            {
                throw new ParseException("Missing closing parenthesis");
            }

            return expression;
        }

        public double Evaluate()
        {
            throw new System.NotImplementedException();
        }
    }
}
