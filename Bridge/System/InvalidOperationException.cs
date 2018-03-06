namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class InvalidOperationException : Exception, Bridge.IBridgeClass
    {
        public extern InvalidOperationException();

        public extern InvalidOperationException(string message);

        public extern InvalidOperationException(string message, Exception innerException);
    }
}