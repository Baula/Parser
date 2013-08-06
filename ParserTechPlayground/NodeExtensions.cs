using System;

namespace ParserTechPlayground
{
    public static class NodeExtensions
    {
        public static T As<T>(this INode node)
            where T : INode
        {
            if (node is T)
                return (T)node;
            throw new InvalidCastException(string.Format("Cannot cast {0} to {1}", node.GetType().Name, typeof(T).Name));
        }
    }
}