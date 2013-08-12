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
            throw new InvalidCastException(string.Format("Cannot cast {0} {1} to {2}", node.GetType().Name, node, typeof(T).Name));
        }
    }
}