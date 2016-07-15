//using System;
//using System.Text;
//using ApprovalTests;
//using ApprovalTests.Reporters;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SimpleParser.Tools;

//namespace SimpleParser.Tests
//{
//    [UseReporter(typeof(DiffReporter))]
//    [TestClass]
//    public class ApprovalTests
//    {
//        [TestMethod]
//        public void RefactoringRegression_ValidFormulas()
//        {
//            var sampleValidFormulas = new[]
//                {
//                    "1+((2+3)*4)^5",
//                    "1+2-3*4/5^6",
//                    "(1+2)/3",
//                    "  (1+3)  ",
//                    "-123",
//                    "1+2*(-3)",
//                    "1+2*( - 3)",
//                    "12.34",
//                    ".34",
//                    "-123+456",
//                    "  (  123 + 456 )  ",
//                    "-.34",
//                    "-12.34",
//                    "-(123+456)",
//                    "(1+2)*(3+4)",  // refer to http://www.3till7.net/2012/04/15/fun-with-gtk/
//                };

//            var sb = new StringBuilder();
//            foreach (var formula in sampleValidFormulas)
//            {
//                var f = Parser.ParseFormula(formula);
//                f.DumpRecursive(sb);
//                sb.Append("==================================" + Environment.NewLine);
//            }

//            Approvals.Verify(sb.ToString());
//        }

//        [TestMethod]
//        public void RefactoringRegression_InvalidFormulas()
//        {
//            var sb = new StringBuilder();
//            var sampleInvalidFormulas = new[]
//                {
//                    "-(123+)",
//                    "-(*123)",
//                    "*123",
//                    "*123a",
//                    "1.",
//                    "--1",
//                };
//            foreach (var formula in sampleInvalidFormulas)
//            {
//                var exceptionThrown = false;
//                try
//                {
//                    Parser.ParseFormula(formula);
//                }
//                catch (ParserException e)
//                {
//                    exceptionThrown = true;
//                    sb.Append(String.Format("Parsing >{0}< Exception: {1}", formula, e.Message) +
//                              Environment.NewLine);
//                }
//                if (!exceptionThrown)
//                    sb.Append(String.Format("Parsing >{0}< Should have thrown exception, but did not",
//                                            formula) + Environment.NewLine);
//            }

//            Approvals.Verify(sb.ToString());
//        }
//    }
//}
