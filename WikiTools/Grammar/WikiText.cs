using System.Collections.Generic;

namespace WikiTools.Grammar
{
    public class WikiText
    {
        private IEnumerable<IToken> _tokens;
        private List<Line> _lines;

        public WikiText(IEnumerable<IToken> tokens)
        {
            var tokenEnum = tokens.GetEnumerator();
            _lines = new List<Line>();
            var line = Line.Produce(tokenEnum);
            while (line != null)
            {
                _lines.Add(line);
                line = Line.Produce(tokenEnum);
            }
            this._tokens = tokens;
        }

        public List<Line> Lines { get { return _lines; } }
    }
}
