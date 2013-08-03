namespace ParserTechPlayground
{
    public class AddSub : IToken
    {
        private Value _left;
        private PluMin _pluMinOp;
        private Expression  _right;

        private AddSub(Value left, PluMin pluMinOp, Expression right)
        {
            _left = left;
            _pluMinOp = pluMinOp;
            _right = right;
        }

        public Value Left { get { return _left; } }
        public PluMin Operator { get { return _pluMinOp; } }
        public Expression Right { get { return _right; } }

        // AddSub       : Value PluMin Expression
        internal static AddSub Produce(TokenBuffer tokens)
        {
            tokens.SavePosition();
            var lhs = Value.Produce(tokens);
            if (lhs != null)
            {
                var pluMin = PluMin.Produce(tokens);
                if (pluMin != null)
                {
                    var rhs = Expression.Produce(tokens);
                    if (rhs != null)
                        return new AddSub(lhs, pluMin, rhs);
                }
            }

            tokens.RestorePosition();
            return null;
        }
    }
}
