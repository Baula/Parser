using Microsoft.VisualStudio.TestTools.UnitTesting;
using WikiTools.Grammar;
using FluentAssertions;
using WikiTools;

namespace WikiTools.Tests
{
    [TestClass]
    public class WikiParserTests
    {
        [TestMethod]
        public void SingleLineWithEOF()
        {
            var parser = CreateParser();

            // Act
            var actual = parser.Parse("This is a single line ended by eof");

            actual.Lines.Count.Should().Be(1);
            actual.Lines[0].Value.Should().Be("This is a single line ended by eof");
        }

        [TestMethod]
        public void SingleLineWithEOL()
        {
            var parser = CreateParser();

            // Act
            var actual = parser.Parse("This is a single line ended by eol\r\n");

            actual.Lines.Count.Should().Be(1);
            actual.Lines[0].Value.Should().Be("This is a single line ended by eol");
        }

        private static WikiParser CreateParser()
        {
            return new WikiParser(new Tokenizer());
        }
    }
}
