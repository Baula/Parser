using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParserTechPlayground
{
    [DebuggerDisplay("{DebuggerString}")]
    class TokenBuffer
    {
        private const int PositionNotSaved = -1;
        private readonly List<IToken> _tokens;
        private int _pos;
        private int _savedPos = PositionNotSaved;

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
            where T : IToken
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
                var content = _tokens.Take(_pos).Aggregate("", (s, t) => s += t.ToString()) +
                              "|" + Current.ToString() + "|" +
                              _tokens.Skip(_pos + 1).Aggregate("", (s, t) => s += t.ToString());

                if (_savedPos == PositionNotSaved)
                    return content;

                var savedPosInfo = string.Format("{0} ({1})", _savedPos, _tokens[_savedPos]);
                return string.Format("{0} savedPos: {1}", content, savedPosInfo);
            }
        }
    }
}
