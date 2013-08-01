using System.Collections.Generic;

namespace WikiTools.Grammar
{
    public class WikiText
    {
        private IEnumerable<IToken> _tokens;
        private List<Line> _lines;

        public WikiText(IEnumerable<IToken> tokens)
        {
            _lines = new List<Line>();
            var line = Line.Produce(tokens);
            _lines.Add(line);
            this._tokens = tokens;
        }

        public List<Line> Lines { get { return _lines; } }
    }
}
