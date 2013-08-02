using System;

namespace ParserTechPlayground
{
    class ParseException : Exception
    {
        public ParseException(string message)
            : base(message)
        { }
    }
}
