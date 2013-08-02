namespace ParserTechPlayground
{
    public class AddSub : IToken
    {
        private Value  _left;
        private PluMin _pluMinOp;
        private Value  _right;

        private AddSub(Value left, PluMin pluMinOp, Value right)
        {
            // TODO: Complete member initialization
            _left = left;
            _pluMinOp = pluMinOp;
            _right = right;
        }

        public Value Left { get { return _left; } }
        public PluMin Operator { get { return _pluMinOp; } }
        public Value Right { get { return _right; } }

        // AddSub       : Value PluMin Value
        internal static AddSub Produce(TokenBuffer tokens)
        {
            tokens.SavePosition();
            var lhs = Value.Produce(tokens);
            if (lhs != null)
            {
                var pluMin = PluMin.Produce(tokens);
                if (pluMin != null)
                {
                    var rhs = Value.Produce(tokens);
                    if (rhs != null)
                        return new AddSub(lhs, pluMin, rhs);
                }
            }

            tokens.RestorePosition();
            return null;
        }
    }
}
