using System;

namespace AssignmentParserPlayground
{
    class ParseException : Exception
    {
        public ParseException(string message)
            : base(message)
        { }
    }
}
