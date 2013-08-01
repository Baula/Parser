using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WikiTools.Grammar
{
    public class Line
    {
        public string Value { get; set; }

        internal static Line Produce(IEnumerable<IToken> tokens)
        {
            var sb = new StringBuilder();
            foreach (var c in tokens.TakeWhile(t => t is Character).Cast<Character>())
            {
                sb.Append(c.Value);
            }
            var line = new Line { Value = sb.ToString() };
            return line;
        }
    }
}
