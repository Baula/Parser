using System.Collections.Generic;
using System.Linq;

namespace ParserTechPlayground
{
    public class Identifier : INode
    {
        private string _name;

        private Identifier(List<Character> characters)
        {
            _name = characters.Aggregate("", (s, c) => s += c.Value);
            Symbols.Register(this);
        }

        public string Name { get { return _name; } }

        // Identifier   : Character+
        internal static Identifier Produce(TokenBuffer tokens)
        {
            var characters = new List<Character>();
            var character = tokens.GetTerminal<Character>();
            if (character != null)
            {
                while (character != null)
                {
                    characters.Add(character);
                    character = tokens.GetTerminal<Character>();
                }

                return new Identifier(characters);
            }

            return null;
        }

        public double Evaluate()
        {
            return Symbols.Get(this.Name).Evaluate();
        }
    }
}
