
namespace WikiTools
{
    public interface IToken
    {
    }

    public class EOF : IToken
    {
    }

    public class Character : IToken
    {
        private char _char;

        public Character(char c)
        {
            this._char = c;
        }

        public char Value { get { return _char; } }
    }

    public class EOL : IToken
    {
    }
}
