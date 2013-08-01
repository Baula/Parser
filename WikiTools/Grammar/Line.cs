using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WikiTools.Grammar
{
    public class Line
    {
        public string Value { get; set; }
        
        private Line ()
	    { }

        internal static Line Produce(IEnumerator<IToken> tokenEnum)
        {
            if (tokenEnum.Current is EOF)
                return null;

            return new Line
            {
                Value = tokenEnum.TakeWhileOfType<IToken, Character>()
                                 .Aggregate<Character, string>("", (s, c) => s += c.Value)
            };
        }
    }
}