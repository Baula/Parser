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
            INode lhs = mulDiv;
            var pluMin = PluMin.Produce(tokens);
            while (pluMin != null)
            {
                var rhs = MulDiv.Produce(tokens);
                if (rhs == null)
                {
                    tokens.RestorePosition();
                    break;
                }
                lhs = new AddSub(lhs, pluMin, rhs);
                pluMin = PluMin.Produce(tokens);
            }

            return lhs;
        }
    }
}
