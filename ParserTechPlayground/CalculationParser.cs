using System.Collections.Generic;

namespace ParserTechPlayground
{
    /*
         Grammar:
     * 
     * - Non-Terminals
     * Assignment   : Identifier AssignOp Expression EOF
     * Expression   : AddSub
     * AddSub       : MulDiv (AddSubOp MulDiv)*
     * MulDiv       : Value (MulDivOp Value)*
     * Value        : Identifier | Number
     * Number       : Digits
     * Digits       : Digit+
     * Identifier   : Character+
     * 
     * - Terminals
     * AddSubOp     : AddOp | SubOp
     * AddOp        : '+'
     * SubOp        : '-'
     * MulDivOp       : MulOp | DivOp
     * MulOp        : '*'
     * DivOp        : '/'
     * Digit        : '0'..'9'
     * Character    : 'a'..'b'
     * AssignOp     : '='
     * EOF
    */
    public class CalculationParser
    {
        public CalculationParser()
        {
            Symbols.Clear();
        }

        public Assignment Parse(string input)
        {
            var tokens = new Tokenizer().Tokenize(input);

            var assignment = Assignment.Produce(tokens);
            if (assignment == null)
                throw new ParseException("Expected assignment.");

            var eof = tokens.GetTerminal<EOF>();
            if (eof == null)
                throw new ParseException("Expected end of file");

            Symbols.Add(assignment.Assignee, assignment.Assigner);

            return assignment;
        }
    }
}
