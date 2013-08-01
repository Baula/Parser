using WikiTools.Grammar;

namespace WikiTools
{
    public class WikiParser
    {
        private readonly ITokenizer _tokenizer;

        public WikiParser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public WikiText Parse(string inputText)
        {
            var tokens = _tokenizer.Tokenize(inputText);
            return new WikiText(tokens);
        }
    }
}
