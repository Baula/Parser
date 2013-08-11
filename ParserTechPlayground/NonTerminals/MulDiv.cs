namespace ParserTechPlayground
{
    public class MulDiv : INode
    {
        private readonly INode _left;
        private readonly TimDiv _timDivOp;
        private readonly Value _right;

        public MulDiv(INode left, TimDiv timDiv, Value right)
        {
            _left = left;
            _timDivOp = timDiv;
            _right = right;
        }

        public INode Left { get { return _left; } }
        public TimDiv Operator { get { return _timDivOp; } }
        public Value Right { get { return _right; } }

        // Value (TimDiv Value)*
        internal static INode Produce(TokenBuffer tokens)
        {
            var value = Value.Produce(tokens);
            if (value == null)
                return null;

            tokens.SavePosition();

            return BuildSubNodes(value, tokens);
        }

        private static INode BuildSubNodes(INode lhs, TokenBuffer tokens)
        {
            var timDiv = TimDiv.Produce(tokens);
            if (timDiv == null)
                return lhs;

            var rhs = Value.Produce(tokens);
            if (rhs == null)
            {
                tokens.RestorePosition();
                return lhs;
            }

            lhs = new MulDiv(lhs, timDiv, rhs);

            return BuildSubNodes(lhs, tokens);
        }

        public double Evaluate()
        {
            if (_timDivOp.IsTimes)
                return _left.Evaluate() * _right.Evaluate();
            return _left.Evaluate() / _right.Evaluate();
        }
    }
}
