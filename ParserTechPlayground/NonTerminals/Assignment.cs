namespace ParserTechPlayground
{
    // Assignment   : Identifier AssignOp Expression EOF
    public class Assignment
    {
        private Identifier _assignee;
        private Expression _assigner;

        private Assignment(Identifier assignee, Expression assigner)
        {
            _assignee = assignee;
            _assigner = assigner;
        }

        public Identifier Assignee
        {
            get { return _assignee; }
        }

        public Expression Assigner
        {
            get { return _assigner; }
        }

        internal static Assignment Produce(TokenBuffer tokens)
        {
            var assignee = Identifier.Produce(tokens);
            if (assignee == null)
                //throw new ParseException("Assignment must start with a identifier for the assignee.");
                return null;

            var assignOp = tokens.GetTerminal<AssignmentOperator>();
            if (assignOp == null)
                //throw new ParseException("Expected assignment operator.");
                return null;

            var assigner = Expression.Produce(tokens);
            if (assigner == null)
                //throw new ParseException("Expected expression for the assigner after assignment operator.");
                return null;

            return new Assignment(assignee, assigner);
        }
    }
}
