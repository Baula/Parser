using System;

namespace SimpleParser
{
    public class ParserException : Exception
    {
        public ParserException(string message) 
            : base(message)
        {
        }
    }
}