namespace ParserTechPlayground
{
    public class AddSub : IAddSubLeftNode
    {
        private IAddSubLeftNode _left;
        private PluMin _pluMinOp;
        private Value _right;

        public AddSub(Value left)
        {
            _left = left;
        }

        private AddSub(IAddSubLeftNode left, PluMin pluMinOp, Value right)
        {
            _left = left;
            _pluMinOp = pluMinOp;
            _right = right;
        }

        public IAddSubLeftNode Left { get { return _left; } }
        public PluMin Operator { get { return _pluMinOp; } }
        public Value Right { get { return _right; } }

        // AddSub       : Value (PluMin Value)*
        internal static AddSub Produce(TokenBuffer tokens)
        {
            IAddSubLeftNode lhs = Value.Produce(tokens);
            if (lhs != null)
            {
                tokens.SavePosition();
                var pluMin = PluMin.Produce(tokens);
                while (pluMin != null)
                {
                    var rhs = Value.Produce(tokens);
                    if (rhs != null)
                    {
                        lhs = new AddSub(lhs, pluMin, rhs);
                        pluMin = PluMin.Produce(tokens);
                    }
                    else
                    {
                        tokens.RestorePosition();
                        break;
                    }
                }
                if (lhs is Value)
                    return new AddSub((Value)lhs);
                return (AddSub)lhs;
            }

            return null;
        }
    }
}
