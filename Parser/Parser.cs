using System;
using System.Linq;
using System.Text;
using SimpleParser.Grammar;
using SimpleParser.Grammar.NonTerminals;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser
{
    public static class Parser
    {
        public static Symbol ParseFormula(string s)
        {
            var symbols = s.Select(c =>
                {
                    switch (c)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9':
                            return new DecimalDigit(c);
                        case ' ':
                            return new WhiteSpace();
                        case '+':
                            return new Plus();
                        case '-':
                            return new Minus();
                        case '*':
                            return new Asterisk();
                        case '/':
                            return new ForwardSlash();
                        case '^':
                            return new Caret();
                        case '.':
                            return new FullStop();
                        case '(':
                            return new OpenParenthesis();
                        case ')':
                            return new CloseParenthesis();
                        default:
                            return (Symbol) null;
                    }
                });
#if false
            if (symbols.Any())
            {
                Console.WriteLine("Terminal Symbols");
                Console.WriteLine("================");
                foreach (var terminal in symbols)
                    Console.WriteLine("{0} >{1}<", terminal.GetType().Name.ToString(),
                        terminal.ToString());
                Console.WriteLine();
            }
#endif
            var formula = Formula.Produce(symbols);
            if (formula == null)
                throw new ParserException("Invalid formula");
            return formula;
        }

        public static void DumpSymbolRecursive(StringBuilder sb, Symbol symbol, int depth)
        {
            sb.Append(string.Format("{0}{1} >{2}<",
                                    "".PadRight(depth*2),
                                    symbol.GetType().Name,
                                    symbol)).Append(Environment.NewLine);
            if (symbol.ConstituentSymbols != null)
                foreach (var childSymbol in symbol.ConstituentSymbols)
                    DumpSymbolRecursive(sb, childSymbol, depth + 1);
        }
    }
}