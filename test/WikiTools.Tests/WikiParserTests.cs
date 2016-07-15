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

            actual.Lines.Count.Should().Be(2);
            actual.Lines[0].Value.Should().Be("This is a single line ended by eol");
            actual.Lines[1].Value.Should().Be("");
        }

        [TestMethod]
        public void TwoLinesWithEOF()
        {
            var parser = CreateParser();

            // Act
            var actual = parser.Parse("This is the 1st line\r\nThis is the last line (ended by EOL).");

            actual.Lines.Count.Should().Be(2);
            actual.Lines[0].Value.Should().Be("This is the 1st line");
            actual.Lines[1].Value.Should().Be("This is the last line (ended by EOL).");
        }

        [TestMethod]
        public void SeveralLines()
        {
            var parser = CreateParser();

            // Act
            var actual = parser.Parse("\r\n2: Started with an empty line\r\n3: Line #three\r\n\r\n5: That was empty again\r\n6: Line #6");

            actual.Lines.Count.Should().Be(6);
            actual.Lines[0].Value.Should().Be("");
            actual.Lines[1].Value.Should().Be("2: Started with an empty line");
            actual.Lines[2].Value.Should().Be("3: Line #three");
            actual.Lines[3].Value.Should().Be("");
            actual.Lines[4].Value.Should().Be("5: That was empty again");
            actual.Lines[5].Value.Should().Be("6: Line #6");
        }

        private static WikiParser CreateParser()
        {
            return new WikiParser(new Tokenizer());
        }
    }
}
