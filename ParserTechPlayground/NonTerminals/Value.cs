using System;
using System.Diagnostics;

namespace ParserTechPlayground
{
    [DebuggerDisplay("{DebuggerDisplayText}")]
    public class Value : INode
    {
        private readonly Identifier _identifier;
        private readonly Number _number;

        private Value(Identifier identifier)
        {
            _identifier = identifier;
        }

        private Value(Number number)
        {
            _number = number;
        }

        public Identifier Identifier { get { return _identifier; } }
        public Number Number { get { return _number; } }

        // Value        : Identifier | Number
        internal static Value Produce(TokenBuffer tokens)
        {
            var identifier = Identifier.Produce(tokens);
            if (identifier != null)
                return new Value(identifier);

            var number = Number.Produce(tokens);
            if (number != null)
                return new Value(number);

            return null;
        }

        public override string ToString()
        {
            if (_number != null)
                return _number.Value.ToString();
            return _identifier.Name;
        }

        internal string DebuggerDisplayText
        {
            get
            {
                var kindInfo = (_number != null) ? "Number" : "Identifier";
                return string.Format("{0} ({1})", ToString(), kindInfo);
            }
        }

        public double Evaluate()
        {
            if (_number != null)
                return _number.Value;
            return _identifier.Evaluate();
        }
    }
}
