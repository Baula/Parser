using System.Globalization;

namespace SimpleParser.Grammar.Terminals
{
    public class DecimalDigit : Symbol
    {
        private readonly string _characterValue;

        public DecimalDigit(char c)
        {
            _characterValue = c.ToString(CultureInfo.InvariantCulture);
        }

        public double Evaluate()
        {
            return int.Parse(_characterValue);
        }

        public override string ToString()
        {
            return _characterValue;
        }
    }
}