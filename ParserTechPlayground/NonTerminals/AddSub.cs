namespace ParserTechPlayground
{
    public class AddSub : INode
    {
        private INode _left;
        private PluMin _pluMinOp;
        private Value _right;

        public AddSub(Value left)
        {
            _left = left;
        }

        private AddSub(INode left, PluMin pluMinOp, Value right)
        {
            _left = left;
            _pluMinOp = pluMinOp;
            _right = right;
        }

        public INode Left { get { return _left; } }
        public PluMin Operator { get { return _pluMinOp; } }
        public Value Right { get { return _right; } }

        // AddSub       : Value (PluMin Value)*
        internal static INode Produce(TokenBuffer tokens)
        {
            var value = Value.Produce(tokens);
            if (value == null)
                return null;

            tokens.SavePosition();
            INode lhs = value;
            var pluMin = PluMin.Produce(tokens);
            while (pluMin != null)
            {
                var rhs = Value.Produce(tokens);
                if (rhs == null)
                {
                    tokens.RestorePosition();
                    break;
                }
                lhs = new AddSub(lhs, pluMin, rhs);
                pluMin = PluMin.Produce(tokens);
            }

            if (lhs is Value)
                return lhs;
            return lhs.As<AddSub>();
        }
    }
}
