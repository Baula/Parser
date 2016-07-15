using System;
using System.Collections.Generic;

namespace SimpleParser.Tools
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source,
                                                 int count)
        {
            var saveList = new Queue<T>();
            var saved = 0;
            foreach (var item in source)
            {
                if (saved < count)
                {
                    saveList.Enqueue(item);
                    ++saved;
                    continue;
                }
                saveList.Enqueue(item);
                yield return saveList.Dequeue();
            }
        }

        public static IEnumerable<TResult> Rollup<TSource, TResult>(this IEnumerable<TSource> source,
                                                                    TResult seed,
                                                                    Func<TSource, TResult, TResult> projection)
        {
            var nextSeed = seed;
            foreach (var src in source)
            {
                var projectedValue = projection(src, nextSeed);
                nextSeed = projectedValue;
                yield return projectedValue;
            }
        }
    }
}