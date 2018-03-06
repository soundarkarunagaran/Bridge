namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class NullReferenceException : Exception, Bridge.IBridgeClass
    {
        public extern NullReferenceException();

        public extern NullReferenceException(string message);

        public extern NullReferenceException(string message, Exception innerException);
    }
}