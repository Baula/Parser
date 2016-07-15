namespace ParserTechPlayground
{
    // Number       : Digits
    public class Number
    {
        private long _value;

        private Number(Digits digits)
        {
            _value = digits.AsWholeNumber;
        }

        public long Value { get { return _value; } }

        internal static Number Produce(TokenBuffer tokens)
        {
            var digits = Digits.Produce(tokens);
            if (digits != null)
                return new Number(digits);

            return null;
        }
    }
}
