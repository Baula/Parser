using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WikiTools.Grammar
{
    public class Line
    {
        public string Value { get; set; }

        internal static Line Produce(IEnumerator<IToken> tokenEnum)
        {
            if (tokenEnum.Current is EOF)
                return null;

            var sb = new StringBuilder();
            while (tokenEnum.MoveNext())
            {
                var c = tokenEnum.Current as Character;
                if (c == null)
                    break;
                sb.Append(c.Value);
            }
            return new Line { Value = sb.ToString() };
        }
    }
}
