using System.Collections.Generic;
using System.Linq;

namespace ParserTechPlayground
{
    public class Identifier
    {
        private List<Character> _characters;
        private string _name;

        public Identifier(List<Character> characters)
        {
            _characters = characters;
            _name = characters.Aggregate("", (s, c) => s += c.Value);
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
