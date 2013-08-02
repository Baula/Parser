using System.Collections.Generic;

namespace ParserTechPlayground
{
    class TokenBuffer
    {
        private readonly List<IToken> _tokens;
        private int _pos;

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
    }
}
