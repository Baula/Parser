using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using WikiTools;

namespace WikiTools.Tests
{
    [TestClass]
    public class TokenizerTests
    {
        ITokenizer _tokenizer;

        [TestInitialize]
        public void SetupTokenizer()
        {
            _tokenizer = new Tokenizer();
        }

        [TestMethod]
        public void EmptyString_ReturnsJustEOF()
        {
            var actual = _tokenizer.Tokenize("");

            actual.Count().Should().Be(1);
            actual.Single().Should().BeOfType<EOF>();
        }

        [TestMethod]
        public void SingleLetterCharacter_ReturnsCharacterAndEOF()
        {
            var actual = _tokenizer.Tokenize("a");

            actual.Count().Should().Be(2);
            
            var charToken = actual.First();
            charToken.Should().BeOfType<Character>();
            charToken.As<Character>().Value.Should().Be('a');

            actual.Last().Should().BeOfType<EOF>();
        }

        [TestMethod]
        public void AllLowerCaseLetters_ReturnCharacters()
        {
            AssertAreAllCharacters("abcdefghijklmnopqrstuvwxyz");
        }

        [TestMethod]
        public void AllUpperCaseLetters_ReturnCharacters()
        {
            AssertAreAllCharacters("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        [TestMethod]
        public void SpaceAndTabCharacters_ReturnCharacters()
        {
            AssertAreAllCharacters(" \t");
        }

        [TestMethod]
        public void CrLf_ReturnsEOL()
        {
            var actual = _tokenizer.Tokenize("\r\n");

            actual.Count().Should().Be(2);

            var firstToken = actual.First();
            firstToken.Should().BeOfType<EOL>();
        }

        private void AssertAreAllCharacters(string allLowercaseLetters)
        {
            foreach (var c in allLowercaseLetters)
            {
                var letter = c.ToString();

                // Act
                var actual = _tokenizer.Tokenize(letter);

                var firstToken = actual.First();
                firstToken.Should().BeOfType<Character>();
                var charToken = firstToken.As<Character>();
                charToken.Value.Should().Be(c);
            }
        }
    }
}
