using System;
using System.Collections.Generic;

namespace ParserTechPlayground
{
    public static class Symbols
    {
        private static readonly IDictionary<string, INode> _symbols = new Dictionary<string, INode>();

        public static void Clear()
        {
            _symbols.Clear();
        }

        public static INode Get(string name)
        {
            if (!_symbols.ContainsKey(name))
                throw new ArgumentOutOfRangeException("Unknown symbol \"" + name + "\".");
            return _symbols[name];
        }

        internal static void Register(Identifier identifier)
        {
            if (!_symbols.ContainsKey(identifier.Name))
                _symbols.Add(identifier.Name, null);
        }

        internal static void Add(Identifier identifier, INode node)
        {
            if (_symbols.ContainsKey(identifier.Name))
                _symbols[identifier.Name] = node;
            else
                _symbols.Add(identifier.Name, node);
        }
    }
}
