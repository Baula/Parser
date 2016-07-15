using System.Collections.Generic;
using System.Linq;
using SimpleParser.Grammar;
using SimpleParser.Grammar.NonTerminals;
using SimpleParser.Grammar.Terminals;

namespace SimpleParser
{
    public static class Parser
    {
        public static Formula ParseFormula(string s)
        {
            var symbols = GetSymbols(s);
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

        private static IEnumerable<Symbol> GetSymbols(string s)
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
            return symbols;
        }
    }
}