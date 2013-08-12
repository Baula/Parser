namespace ParserTechPlayground
{
    public class ExpRootOp 
    {
        private ExponentiationOperator _expOp;
        private RootExtractionOperator _rootOp;

        public ExpRootOp(ExponentiationOperator expOp)
        {
            _expOp = expOp;
        }

        public ExpRootOp(RootExtractionOperator rootOp)
        {
            _rootOp = rootOp;
        }

        public bool IsExponentiation { get { return _expOp != null; } }
        public bool IsRootExtraction { get { return _rootOp != null; } }

        // ExpRootOp    : ExpOp | RootOp
        internal static ExpRootOp Produce(TokenBuffer tokens)
        {
            var expOp = tokens.GetTerminal<ExponentiationOperator>();
            if (expOp != null)
                return new ExpRootOp(expOp);

            var rootOp = tokens.GetTerminal<RootExtractionOperator>();
            if (rootOp != null)
                return new ExpRootOp(rootOp);

            return null;
        }
    }
}
