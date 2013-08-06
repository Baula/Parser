using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WikiTools.Tests.Grammar
{
    [TestClass]
    public class LineTests
    {
        [TestMethod]
        public void JustEOF_GivesALineWithEmptyValue()
        {
            var tokens = new IToken[] { new EOF() };
            var enumer = tokens.Cast<IToken>().GetEnumerator();
            enumer.MoveNext();

            // Act
            var line = WikiTools.Grammar.Line.Produce(enumer);

            Assert.Inconclusive("wip");
            Assert.IsNotNull(line);
            Assert.AreEqual(0, line.Value);
        }
    }
}
