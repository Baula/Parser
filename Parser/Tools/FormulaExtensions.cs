using System;
using System.Text;
using SimpleParser.Grammar;

namespace SimpleParser.Tools
{
    static class FormulaExtensions
    {
        public static void DumpRecursive(this Symbol symbol, StringBuilder sb, int depth = 0)
        {
            sb.Append(string.Format("{0}{1} >{2}<",
                                    "".PadRight(depth * 2),
                                    symbol.GetType().Name,
                                    symbol)).Append(Environment.NewLine);
            if (symbol.ConstituentSymbols != null)
                foreach (var childSymbol in symbol.ConstituentSymbols)
                    DumpRecursive(childSymbol, sb, depth + 1);
        }
    }
}
