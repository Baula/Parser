using System.Collections.Generic;

namespace ParserTechPlayground
{
    /*
         Grammar:
     * 
     * - Non-Terminals
     * Assignment   : Identifier AssignOp Expression EOF
     * Expression   : Value
     * Value        : Identifier | Number
     * Number       : Digits
     * Digits       : Digit+
     * Identifier   : Character+
     * 
     * - Terminals
     * Digit        : '0'..'9'
     * Character    : 'a'..'b'
     * AssignOp     : '='
     * EOF
    */
    public class CalculationParser
    {
        public Assignment Parse(string input)
        {
            var tokens = new Tokenizer().Tokenize(input);

            var assignment = Assignment.Produce(tokens);
            if (assignment == null)
                throw new ParseException("Expected assignment.");

            var eof = tokens.GetTerminal<EOF>();
            if (eof == null)
                throw new ParseException("Expected end of file");

            return assignment;
        }
    }
}
