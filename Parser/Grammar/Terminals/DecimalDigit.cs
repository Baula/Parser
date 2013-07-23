namespace SimpleParser.Grammar.Terminals
{
    public class DecimalDigit : Symbol
    {
        private readonly string _characterValue;

        public DecimalDigit(char c)
        {
            _characterValue = c.ToString();
        }

        public override string ToString()
        {
            return _characterValue;
        }
    }
}