using System;
namespace ParserTechPlayground
{
    public class ExpRoot : INode
    {
        private INode _left;
        private ExpRootOp _operator;
        private INode _right;

        public ExpRoot(INode left, ExpRootOp @operator, INode right)
        {
            _left = left;
            _operator = @operator;
            _right = right;
        }
        public INode Left { get { return _left; } }
        public ExpRootOp Operator { get { return _operator; } }
        public INode Right { get { return _right; } }

        // ExpRoot      : Unary ExpRootOp ExpRoot | Unary
        internal static INode Produce(TokenBuffer tokens)
        {
            var unary = Unary.Produce(tokens);
            if (unary == null)
                return null;

            tokens.SavePosition();
            var expRootOp = ExpRootOp.Produce(tokens);
            if (expRootOp == null)
                return unary;

            var rhs = ExpRoot.Produce(tokens);
            if (rhs != null)
                return new ExpRoot(unary, expRootOp, rhs);

            tokens.RestorePosition();
            return null;
        }
        
        public double Evaluate()
        {
            if (_operator.IsExponentiation)
                return Math.Pow(_left.Evaluate(), _right.Evaluate());
            return Math.Pow(_left.Evaluate(), 1 / _right.Evaluate());
        }
    }
}
