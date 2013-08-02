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
            var tokens = new Tokenizer().Tokenize(input);

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

        private EOF GetEOF(TokenBuffer tokens)
        {
            return Get<EOF>(tokens);
        }

        private AssignmentOperator GetAssignOp(TokenBuffer tokens)
        {
            return Get<AssignmentOperator>(tokens);
        }

        private Character GetCharacter(TokenBuffer tokens)
        {
            return Get<Character>(tokens);
        }

        private static T Get<T>(TokenBuffer tokens)
            where T : IToken
        {
            if (tokens.Current is T)
                return (T)tokens.GetAndConsumeCurrent();
            return default(T);
        }
    }
}
