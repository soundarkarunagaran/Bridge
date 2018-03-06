namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class InvalidCastException : Exception, Bridge.IBridgeClass
    {
        public extern InvalidCastException();

        public extern InvalidCastException(string message);

        public extern InvalidCastException(string message, Exception innerException);
    }
}