namespace ParserTechPlayground
{
    public class MulDivOp
    {
        private MultiplicationOperator _mulOp;
        private DivisionOperator _divOp;

        public MulDivOp(MultiplicationOperator mulOp)
        {
            _mulOp = mulOp;
        }

        public MulDivOp(DivisionOperator divOp)
        {
            _divOp = divOp;
        }

        // MulDivOp       : MulOp | DivOp
        internal static MulDivOp Produce(TokenBuffer tokens)
        {
            var mulOp = tokens.GetTerminal<MultiplicationOperator>();
            if (mulOp != null)
                return new MulDivOp(mulOp);

            var divOp = tokens.GetTerminal<DivisionOperator>();
            if (divOp != null)
                return new MulDivOp(divOp);

            return null;
        }

        public bool IsTimes { get { return _mulOp != null; } }
        public bool IsDividedBy { get { return _divOp != null; } }
    }
}
