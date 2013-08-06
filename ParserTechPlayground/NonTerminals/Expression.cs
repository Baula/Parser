using System.Diagnostics;

namespace ParserTechPlayground
{
    [DebuggerDisplay("{DebuggerDisplayString}")]
    public class Expression
    {
        private readonly Value _value;
        private readonly AddSub _addSub;

        private Expression(Value value)
        {
            _value = value;
        }

        private Expression(AddSub addSub)
        {
            _addSub = addSub;
        }

        public Value Value { get { return _value; } }
        public AddSub AddSub { get { return _addSub; } }

        // Expression   : AddSub | Value
        internal static Expression Produce(TokenBuffer tokens)
        {
            var addSub = AddSub.Produce(tokens);
            if (addSub != null)
                return new Expression(addSub);

            var value = Value.Produce(tokens);
            if (value != null)
                return new Expression(value);

            return null;
        }

        public override string ToString()
        {
            return (_value != null) ? _value.ToString() : "[" + _addSub.ToString() + "]";
        }

        internal string DebuggerDisplayString
        {
            get
            {
                var kindInfo = (_value != null) ? "Value" : "AddSub";
                return string.Format("{0} ({1})", ToString(), kindInfo);
            }
        }
    }
}
