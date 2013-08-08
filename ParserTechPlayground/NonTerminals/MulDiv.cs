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
            INode lhs = value;
            var timDiv = TimDiv.Produce(tokens);
            while (timDiv != null)
            {
                var rhs = Value.Produce(tokens);
                if (rhs == null)
                {
                    tokens.RestorePosition();
                    break;
                }
                lhs = new MulDiv(lhs, timDiv, rhs);
                timDiv = TimDiv.Produce(tokens);
            }

            if (lhs is Value)
                return lhs;
            return lhs.As<MulDiv>();
        }
    }
}
