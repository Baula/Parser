namespace ParserTechPlayground
{
    public class Expression
    {
        // Expression   : AddSub
        internal static INode Produce(TokenBuffer tokens)
        {
            return AddSub.Produce(tokens);
        }
    }
}
