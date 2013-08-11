namespace ParserTechPlayground
{
    public class AddSubOp
    {
        private readonly bool _isPlusNotMinus;

        private AddSubOp(AdditionOperator addOp)
        {
            _isPlusNotMinus = true;
        }

        private AddSubOp(SubtractionOperator subOp)
        {
            _isPlusNotMinus = false;
        }

        public bool IsPlusNotMinus { get { return _isPlusNotMinus; } }

        // AddSubOp       : AddOp | SubOp
        internal static AddSubOp Produce(TokenBuffer tokens)
        {
            var addOp = tokens.GetTerminal<AdditionOperator>();
            if (addOp != null)
                return new AddSubOp(addOp);

            var subOp = tokens.GetTerminal<SubtractionOperator>();
            if (subOp != null)
                return new AddSubOp(subOp);

            return null;
        }

        public override string ToString()
        {
            return _isPlusNotMinus ? "+" : "-";
        }
    }
}
