namespace ParserTechPlayground
{
    public class Character : ITerminal
    {
        private readonly char _c;

        public Character(char c)
        {
            _c = c;
        }

        public string Value { get { return _c.ToString(); } }

        public override string ToString() { return Value; }
    }
}
