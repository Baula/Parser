using System.Collections.Generic;

namespace ParserTechPlayground
{
    /*
         Grammar:
     * 
     * - Non-Terminals
     * Assignment   : Identifier AssignOp Identifier EOF
     * Identifier   : Character+
     * 
     * - Terminals
     * Character    : 'a'..'b';
     * AssignOp     : '='
     * EOF
    */
    public class CalculationParser
    {
        public Assignment Parse(string input)
        {
            var tokens = Tokenize(input);

            var assignment = GetAssignment(tokens);
            if (assignment == null)
                throw new ParseException("Expected assignment.");

            return assignment;
        }

        private Assignment GetAssignment(TokenBuffer tokens)
        {
            var assignee = GetIdentifier(tokens);
            if (assignee == null)
                throw new ParseException("Assignment must start with a identifier for the assignee.");

            var assignOp = GetAssignOp(tokens);
            if (assignOp == null)
                throw new ParseException("Expected assignment operator.");

            var assigner = GetIdentifier(tokens);
            if (assigner == null)
                throw new ParseException("Expected identifier for the assigner after assignment operator.");

            var eof = GetEOF(tokens);
            if (eof == null)
                throw new ParseException("Expected end of file");

            return new Assignment(assignee, assigner);
        }

        private EOF GetEOF(TokenBuffer tokens)
        {
            if (tokens.Current is EOF)
                return (EOF)tokens.GetAndConsumeCurrent();
            return null;
        }

        private AssignmentOperator GetAssignOp(TokenBuffer tokens)
        {
            if (tokens.Current is AssignmentOperator)
                return (AssignmentOperator)tokens.GetAndConsumeCurrent();
            return null;
        }

        private Identifier GetIdentifier(TokenBuffer tokens)
        {
            var characters = new List<Character>();
            var character = GetCharacter(tokens);
            if (character == null)
                return null;

            while (character != null)
            {
                characters.Add(character);
                character = GetCharacter(tokens);
            }

            return new Identifier(characters);
        }

        private Character GetCharacter(TokenBuffer tokens)
        {
            if (tokens.Current is Character)
                return (Character)tokens.GetAndConsumeCurrent();
            return null;
        }

        private TokenBuffer Tokenize(string input)
        {
            var buffer = new TokenBuffer();
            foreach (var c in input)
            {
                var token = GetToken(c);
                buffer.Add(token);
            }
            buffer.Add(new EOF());
            return buffer;
        }

        private IToken GetToken(char c)
        {
            if (char.IsLetter(c))
                return new Character(c);
            if (c == '=')
                return new AssignmentOperator();
            throw new ParseException(string.Format("Invalid character '{0}'.", c));
        }
    }
}
