namespace ParserTechPlayground
{
    public class PluMin : IToken
    {
        private readonly bool _isPlusNotMinus;

        private PluMin(AdditionOperator addOp)
        {
            _isPlusNotMinus = true;
        }

        private PluMin(SubtractionOperator subOp)
        {
            _isPlusNotMinus = false;
        }

        public bool IsPlusNotMinus { get { return _isPlusNotMinus; } }

        // PluMin       : AddOp | SubOp
        internal static PluMin Produce(TokenBuffer tokens)
        {
            var addOp = tokens.GetTerminal<AdditionOperator>();
            if (addOp != null)
                return new PluMin(addOp);

            var subOp = tokens.GetTerminal<SubtractionOperator>();
            if (subOp != null)
                return new PluMin(subOp);

            return null;
        }
    }
}
