namespace ParserTechPlayground
{
    public class Unary : INode
    {
        private AddSubOp _operator;
        private Value _value;

        public Unary(AddSubOp @operator, Value value)
        {
            _operator = @operator;
            _value = value;
        }

        // Unary        : AddSubOp? Value
        internal static INode Produce(TokenBuffer tokens)
        {
            // HACK
            if (tokens.Current is AdditionOperator || tokens.Current is SubtractionOperator)
                tokens.SavePosition();

            var op = AddSubOp.Produce(tokens);

            var value = Value.Produce(tokens);
            if (value != null)
            {
                if (op != null && op.IsMinus)
                    return new Unary(op, value);
                return value;
            }

            tokens.RestorePosition();
            return null;
        }

        public double Evaluate()
        {
            if (_operator.IsMinus)
                return -_value.Evaluate();
            return _value.Evaluate();
        }
    }
}
