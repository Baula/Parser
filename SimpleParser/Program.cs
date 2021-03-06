﻿using System;
using System.Text;
using SimpleParser.Tools;

namespace SimpleParser
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            TestFormula(args[0]);
            //ShowValidFormulas();
            //ShowInvalidFormulas();
        }

        private static void TestFormula(string formulaAsString)
        {
            try
            {
                var formula = Parser.ParseFormula(formulaAsString);

                Console.WriteLine(formulaAsString);
                Console.WriteLine(formula + " = " + formula.Evaluate());
                //var sb = new StringBuilder();
                //formula.DumpRecursive(sb);
                //Console.WriteLine(sb);

            }
            catch (ParserException e)
            {
                Console.WriteLine("Parsing >{0}< Exception: {1}", formulaAsString, e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void ShowValidFormulas()
        {
            var sampleValidFormulas = new[]
                {
                    "1+((2+3)*4)^5",
                    "1+2-3*4/5^6",
                    "(1+2)/3",
                    "  (1+3)  ",
                    "-123",
                    "1+2*(-3)",
                    "1+2*( - 3)",
                    "12.34",
                    ".34",
                    "-123+456",
                    "  (  123 + 456 )  ",
                    "-.34",
                    "-12.34",
                    "-(123+456)",
                };

            var sb = new StringBuilder();
            foreach (var formula in sampleValidFormulas)
            {
                var f = Parser.ParseFormula(formula);
                f.DumpRecursive(sb);
                sb.Append("==================================" + Environment.NewLine);
            }
            Console.WriteLine(sb.ToString());
        }

        private static void ShowInvalidFormulas()
        {
            var sb = new StringBuilder();
            var sampleInvalidFormulas = new[]
                {
                    "-(123+)",
                    "-(*123)",
                    "*123",
                    "*123a",
                    "1.",
                    "--1",
                };
            foreach (var formula in sampleInvalidFormulas)
            {
                var exceptionThrown = false;
                try
                {
                    Parser.ParseFormula(formula);
                }
                catch (ParserException e)
                {
                    exceptionThrown = true;
                    sb.Append(String.Format("Parsing >{0}< Exception: {1}", formula, e.Message) +
                              Environment.NewLine);
                }
                if (!exceptionThrown)
                    sb.Append(String.Format("Parsing >{0}< Should have thrown exception, but did not",
                                            formula) + Environment.NewLine);
            }
            Console.WriteLine(sb.ToString());
        }
    }
}