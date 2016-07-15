using System;

namespace ParserTechPlayground
{
    public class ParseException : Exception
    {
        public ParseException(string message)
            : base(message)
        { }
    }
}
