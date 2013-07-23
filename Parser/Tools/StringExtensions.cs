using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleParser.Tools
{
    public static class StringExtensions
    {
        public static string StringConcatenate(this IEnumerable<string> source)
        {
            var sb = new StringBuilder();
            foreach (var s in source)
                sb.Append(s);
            return sb.ToString();
        }

        public static string StringConcatenate<T>(
            this IEnumerable<T> source,
            Func<T, string> projectionFunc)
        {
            return source.Aggregate(new StringBuilder(),
                                    (s, i) => s.Append(projectionFunc(i)),
                                    s => s.ToString());
        }
    }
}