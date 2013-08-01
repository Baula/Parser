using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WikiTools
{
    public interface ITokenizer
    {
        IEnumerable<IToken> Tokenize(string inputText);
    }

    public class Tokenizer : ITokenizer
    {
        public IEnumerable<IToken> Tokenize(string inputText)
        {
            var enumer = new ReadAheadEnumerator<char>(inputText);

            while (enumer.MoveNext())
            {
                yield return GetToken(enumer);
                //if (enumer.ReadAhead(1) == '\n')
                //{
                //    enumer.MoveNext();
                //    yield return 
                //}
            }
            //foreach (char c in inputText)
            //{
            //    yield return GetToken(c);
            //}
            yield return new EOF();
        }

        private IToken GetToken(ReadAheadEnumerator<char> enumerator)
        {
            var c = enumerator.Current;
            if (char.IsLetterOrDigit(c))
                return new Character(c);
            if (c == ' ' || c == '\t')
                return new Character(c);
            if (@".:,;-/\(){}#+*".Contains(c))
                return new Character(c);
            if (c == '\r' && enumerator.CanReadAhead && enumerator.ItemAhead == '\n')
            {
                enumerator.MoveNext();
                return new EOL();
            }
            throw new ArgumentOutOfRangeException("c", string.Format("'{0}' is not a supported character.", c));
        }
    }
}
