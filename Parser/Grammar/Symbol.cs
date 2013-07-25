using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Tools;

namespace SimpleParser.Grammar
{
    public abstract class Symbol
    {
        private readonly List<Symbol> _constituentSymbols;

        protected Symbol()
        {
        }

        protected Symbol(params Object[] symbols)
        {
            var ls = new List<Symbol>();
            foreach (var item in symbols)
            {
                if (item is Symbol)
                    ls.Add((Symbol) item);
                else if (item is IEnumerable<Symbol>)
                    ls.AddRange((IEnumerable<Symbol>) item);
                else
                    // If this error is thrown, the parser is coded incorrectly.
                    throw new ParserException("Internal error");
            }
            _constituentSymbols = ls;
        }

        public List<Symbol> ConstituentSymbols
        {
            get { return _constituentSymbols; }
        }

        public override string ToString()
        {
            return ConstituentSymbols.Select(cs => cs.ToString()).Concatenate();
        }
    }
}