namespace ParserTechPlayground
{
    public class Unary : INode
    {
        private AddSubOp _operator;
        private INode _atom;

        public Unary(AddSubOp @operator, INode atom)
        {
            _operator = @operator;
            _atom = atom;
        }

        public INode Operand { get { return _atom; } }

        // Unary        : AddSubOp? Atom
        internal static INode Produce(TokenBuffer tokens)
        {
            // HACK
            if (tokens.Current is AdditionOperator || tokens.Current is SubtractionOperator)
                tokens.SavePosition();

            var op = AddSubOp.Produce(tokens);

            var atom = Atom.Produce(tokens);
            if (atom != null)
            {
                if (op != null && op.IsMinus)
                    return new Unary(op, atom);
                return atom;
            }

            tokens.RestorePosition();
            return null;
        }

        public double Evaluate()
        {
            if (_operator.IsMinus)
                return -_atom.Evaluate();
            return _atom.Evaluate();
        }

        public override string ToString()
        {
            if (_operator != null)
                return _operator.ToString() + _atom.ToString();
            return _atom.ToString();
        }
    }
}
