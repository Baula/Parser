using System.Globalization;

namespace SimpleParser.Grammar.Terminals
{
    public class DecimalDigit : Symbol
    {
        private readonly int _value;

        public DecimalDigit(char c)
        {
            _value = int.Parse(c.ToString());
        }

        public int Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}