namespace ParserTechPlayground
{
    public class TimDiv
    {
        private MultiplicationOperator _mulOp;
        private DivisionOperator _divOp;

        public TimDiv(MultiplicationOperator mulOp)
        {
            _mulOp = mulOp;
        }

        public TimDiv(DivisionOperator divOp)
        {
            _divOp = divOp;
        }

        // TimDiv       : MulOp | DivOp
        internal static TimDiv Produce(TokenBuffer tokens)
        {
            var mulOp = tokens.GetTerminal<MultiplicationOperator>();
            if (mulOp != null)
                return new TimDiv(mulOp);

            var divOp = tokens.GetTerminal<DivisionOperator>();
            if (divOp != null)
                return new TimDiv(divOp);

            return null;
        }

        public bool IsTimes { get { return _mulOp != null; } }
        public bool IsDividedBy { get { return _divOp != null; } }
    }
}
