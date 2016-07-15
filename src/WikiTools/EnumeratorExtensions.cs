using System.Collections.Generic;

namespace WikiTools
{
    public static class EnumeratorExtensions
    {
        public static IEnumerable<TTarget> TakeWhileOfType<T, TTarget>(this IEnumerator<T> enumer)
            where TTarget : T
        {
            while (enumer.MoveNext() && enumer.Current is TTarget)
            {
                yield return (TTarget)enumer.Current;
            }
        }
    }
}

