namespace ParserTechPlayground
{
    public class AddSub
    {
        private Value _leftValue;
        private AddSub _leftAddSub;
        private PluMin _pluMinOp;
        private Value _right;

        public AddSub(Value left)
        {
            _leftValue = left;
        }

        private AddSub(Value left, PluMin pluMinOp, Value right)
        {
            _leftValue = left;
            _pluMinOp = pluMinOp;
            _right = right;
        }

        public AddSub(AddSub left, PluMin pluMin, Value right)
        {
            _leftAddSub = left;
            _pluMinOp = pluMin;
            _right = right;
        }

        public Value LeftValue { get { return _leftValue; } }
        public AddSub LeftAddSub { get { return _leftAddSub; } }
        public PluMin Operator { get { return _pluMinOp; } }
        public Value Right { get { return _right; } }

        // AddSub       : Value (PluMin Value)*
        internal static AddSub Produce(TokenBuffer tokens)
        {
            tokens.SavePosition();   // TODO: think about this
            var lhs = Value.Produce(tokens);
            if (lhs != null)
            {
                AddSub leftAddSub = null;
                var pluMin = PluMin.Produce(tokens);
                while (pluMin != null)
                {
                    var rhs = Value.Produce(tokens);
                    if (rhs != null)
                    {
                        if (leftAddSub == null)
                            leftAddSub = new AddSub(lhs, pluMin, rhs);
                        else
                            leftAddSub = new AddSub(leftAddSub, pluMin, rhs);
                    }
                    pluMin = PluMin.Produce(tokens);
                }
                if (leftAddSub == null)
                    return new AddSub(lhs);
                return leftAddSub;
            }

            tokens.RestorePosition();
            return null;
        }
    }
}
