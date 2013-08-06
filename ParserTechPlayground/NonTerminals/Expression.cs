using System.Diagnostics;

namespace ParserTechPlayground
{
    [DebuggerDisplay("{DebuggerDisplayString}")]
    public class Expression
    {
        private readonly AddSub _addSub;

        private Expression(AddSub addSub)
        {
            _addSub = addSub;
        }

        public AddSub AddSub { get { return _addSub; } }

        // Expression   : AddSub
        internal static Expression Produce(TokenBuffer tokens)
        {
            var addSub = AddSub.Produce(tokens);
            if (addSub != null)
                return new Expression(addSub);

            return null;
        }

        public override string ToString()
        {
            return "[" + _addSub.ToString() + "]";
        }

        internal string DebuggerDisplayString
        {
            get
            {
                return string.Format("{0}", ToString());
            }
        }
    }
}
