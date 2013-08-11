namespace ParserTechPlayground
{
    public class AddSub : INode
    {
        private INode _left;
        private AddSubOp _operator;
        private INode _right;

        public AddSub(Value left)
        {
            _left = left;
        }

        private AddSub(INode left, AddSubOp @operator, INode right)
        {
            _left = left;
            _operator = @operator;
            _right = right;
        }

        public INode Left { get { return _left; } }
        public AddSubOp Operator { get { return _operator; } }
        public INode Right { get { return _right; } }

        // AddSub       : MulDiv (AddSubOp MulDiv)*
        internal static INode Produce(TokenBuffer tokens)
        {
            var mulDiv = MulDiv.Produce(tokens);
            if (mulDiv == null)
                return null;

            tokens.SavePosition();

            return BuildSubNodes(mulDiv, tokens);
        }

        private static INode BuildSubNodes(INode lhs, TokenBuffer tokens)
        {
            var addSubOp = AddSubOp.Produce(tokens);
            if (addSubOp == null)
                return lhs;

            var rhs = MulDiv.Produce(tokens);
            if (rhs == null)
            {
                tokens.RestorePosition();
                return lhs;
            }

            lhs = new AddSub(lhs, addSubOp, rhs);

            return BuildSubNodes(lhs, tokens);
        }

        public double Evaluate()
        {
            if (_operator.IsPlusNotMinus)
                return _left.Evaluate() + _right.Evaluate();
            return _left.Evaluate() - _right.Evaluate();
        }
    }
}
