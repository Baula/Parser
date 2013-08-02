
namespace AssignmentParserPlayground
{
    class Assignment
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
    }
}
