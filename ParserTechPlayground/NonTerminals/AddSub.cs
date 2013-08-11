namespace ParserTechPlayground
{
    public class AddSub : INode
    {
        private INode _left;
        private PluMin _pluMinOp;
        private INode _right;

        public AddSub(Value left)
        {
            _left = left;
        }

        private AddSub(INode left, PluMin pluMinOp, INode right)
        {
            _left = left;
            _pluMinOp = pluMinOp;
            _right = right;
        }

        public INode Left { get { return _left; } }
        public PluMin Operator { get { return _pluMinOp; } }
        public INode Right { get { return _right; } }

        // AddSub       : MulDiv (PluMin MulDiv)*
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
            var pluMin = PluMin.Produce(tokens);
            if (pluMin == null)
                return lhs;

            var rhs = MulDiv.Produce(tokens);
            if (rhs == null)
            {
                tokens.RestorePosition();
                return lhs;
            }

            lhs = new AddSub(lhs, pluMin, rhs);

            return BuildSubNodes(lhs, tokens);
        }

        public double Evaluate()
        {
            if (_pluMinOp.IsPlusNotMinus)
                return _left.Evaluate() + _right.Evaluate();
            return _left.Evaluate() - _right.Evaluate();
        }
    }
}
