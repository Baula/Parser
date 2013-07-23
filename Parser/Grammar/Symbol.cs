using System;
using System.Collections.Generic;
using System.Linq;
using SimpleParser.Tools;

namespace SimpleParser.Grammar
{
    public abstract class Symbol
    {
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
            ConstituentSymbols = ls;
        }

        public List<Symbol> ConstituentSymbols { get; set; }

        public override string ToString()
        {
            var s = ConstituentSymbols.Select(ct => ct.ToString()).StringConcatenate();
            return s;
        }
    }
}