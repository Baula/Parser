using System;
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

            return new Line
            {
                Value = tokenEnum.TakeWhileOfType<IToken, Character>()
                                 .Aggregate<Character, string>("", (s, c) => s += c.Value)
            };
        }
    }

    public static class EnumeratorExtensions
    {
        public static IEnumerable<TTarget> TakeWhileOfType<T, TTarget>(this IEnumerator<T> enumer)
            where TTarget : T
        {
            var l = new List<TTarget>();
            while (enumer.MoveNext() && enumer.Current is TTarget)
            {
                l.Add((TTarget)enumer.Current);
            }
            return l;
        }
    }
}
