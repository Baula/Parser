using System.Collections.Generic;
using System.Linq;

namespace ParserTechPlayground
{
    public class Digits : IToken
    {
        private long _asWholeNumber;

        private Digits(List<Digit> digitList)
        {
            _asWholeNumber = digitList.Aggregate(0L, (s, d) => s = 10 * s + d.Value);
        }

        public long AsWholeNumber { get { return _asWholeNumber; } }

        // Digits       : Digit+
        internal static Digits Produce(TokenBuffer tokens)
        {
            var digitList = new List<Digit>();
            var digit = tokens.GetTerminal<Digit>();
            if (digit != null)
            {
                while (digit != null)
                {
                    digitList.Add(digit);
                    digit = tokens.GetTerminal<Digit>();
                }
                return new Digits(digitList);
            }

            return null;
        }
    }
}
