namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class FormatException : Exception, Bridge.IBridgeClass
    {
        public extern FormatException();

        public extern FormatException(string message);

        public extern FormatException(string message, Exception innerException);
    }
}