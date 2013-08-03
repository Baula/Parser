using System;

namespace ParserTechPlayground
{
    public class Digit : ITerminal
    {
        private int _value;

        public Digit(char c)
        {
            _value = int.Parse(c + "");
        }

        public int Value { get { return _value; } }

        public override string ToString() { return Value.ToString(); }
    }
}
