using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParserTechPlayground
{
    [DebuggerDisplay("{DebuggerString}")]
    class TokenBuffer
    {
        private readonly List<IToken> _tokens;
        private int _pos;
        private int _savedPos;

        public TokenBuffer()
        {
            _tokens = new List<IToken>();
            _pos = 0;
        }

        internal void Add(IToken token)
        {
            _tokens.Add(token);
        }

        public IToken Current
        {
            get { return _tokens[_pos]; }
        }

        internal IToken GetAndConsumeCurrent()
        {
            var t = Current;
            _pos++;
            return t;
        }

        public T GetTerminal<T>()
            where T : ITerminal
        {
            if (Current is T)
                return (T)GetAndConsumeCurrent();
            return default(T);
        }

        internal void SavePosition()
        {
            _savedPos = _pos;
        }

        internal void RestorePosition()
        {
            _pos = _savedPos;
        }

        internal string DebuggerString
        {
            get
            {
                return _tokens.Take(_pos).Aggregate("", (s, t) => s += t.ToString()) +
                       "|" + Current.ToString() + "|" +
                       _tokens.Skip(_pos + 1).Aggregate("", (s, t) => s += t.ToString());
            }
        }
    }
}
