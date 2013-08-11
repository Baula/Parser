namespace ParserTechPlayground
{
    public class MulDiv : INode
    {
        private readonly INode _left;
        private readonly MulDivOp _operator;
        private readonly Value _right;

        public MulDiv(INode left, MulDivOp @operator, Value right)
        {
            _left = left;
            _operator = @operator;
            _right = right;
        }

        public INode Left { get { return _left; } }
        public MulDivOp Operator { get { return _operator; } }
        public Value Right { get { return _right; } }

        // Value (MulDivOp Value)*
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
            var mulDivOp = MulDivOp.Produce(tokens);
            if (mulDivOp == null)
                return lhs;

            var rhs = Value.Produce(tokens);
            if (rhs == null)
            {
                tokens.RestorePosition();
                return lhs;
            }

            lhs = new MulDiv(lhs, mulDivOp, rhs);

            return BuildSubNodes(lhs, tokens);
        }

        public double Evaluate()
        {
            if (_operator.IsTimes)
                return _left.Evaluate() * _right.Evaluate();
            return _left.Evaluate() / _right.Evaluate();
        }
    }
}
