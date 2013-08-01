using System.Collections.Generic;

namespace WikiTools.Grammar
{
    public class WikiText
    {
        private List<Line> _lines;

        private WikiText(IEnumerable<IToken> tokens)
        {
            var tokenEnum = tokens.GetEnumerator();
            _lines = new List<Line>();
            var line = Line.Produce(tokenEnum);
            while (line != null)
            {
                _lines.Add(line);
                line = Line.Produce(tokenEnum);
            }
        }

        public static WikiText Produce(IEnumerable<IToken> tokens)
        {
            return new WikiText(tokens);
        }

        public List<Line> Lines { get { return _lines; } }
    }
}
