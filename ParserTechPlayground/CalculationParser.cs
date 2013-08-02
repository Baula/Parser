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

            var eof = tokens.GetTerminal<EOF>();
            if (eof == null)
                throw new ParseException("Expected end of file");

            return assignment;
        }

        // Assignment   : Identifier AssignOp Identifier EOF
        private Assignment GetAssignment(TokenBuffer tokens)
        {
            var assignee = GetIdentifier(tokens);
            if (assignee == null)
                //throw new ParseException("Assignment must start with a identifier for the assignee.");
                return null;

            var assignOp = tokens.GetTerminal<AssignmentOperator>();
            if (assignOp == null)
                //throw new ParseException("Expected assignment operator.");
                return null;

            var assigner = GetIdentifier(tokens);
            if (assigner == null)
                //throw new ParseException("Expected identifier for the assigner after assignment operator.");
                return null;

            return new Assignment(assignee, assigner);
        }

        // Identifier   : Character+
        private Identifier GetIdentifier(TokenBuffer tokens)
        {
            var characters = new List<Character>();
            var character = tokens.GetTerminal<Character>();
            if (character == null)
                return null;

            while (character != null)
            {
                characters.Add(character);
                character = tokens.GetTerminal<Character>();
            }

            return new Identifier(characters);
        }
    }
}
