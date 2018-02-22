namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class RankException : Exception, Bridge.IBridgeClass
    {
        public extern RankException();

        public extern RankException(string message);

        public extern RankException(string message, Exception innerException);
    }
}