namespace ParserTechPlayground
{
    public class Assignment
    {
        private Identifier _assignee;
        private Identifier _assigner;

        public Assignment(Identifier assignee, Identifier assigner)
        {
            _assignee = assignee;
            _assigner = assigner;
        }

        public Identifier Assignee
        {
            get { return _assignee; }
        }

        public Identifier Assigner
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

            var assigner = Identifier.Produce(tokens);
            if (assigner == null)
                //throw new ParseException("Expected identifier for the assigner after assignment operator.");
                return null;

            return new Assignment(assignee, assigner);
        }
    }
}
