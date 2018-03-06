namespace System.Collections.Generic
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class KeyNotFoundException : Exception, Bridge.IBridgeClass
    {
        public extern KeyNotFoundException();

        public extern KeyNotFoundException(string message);

        public extern KeyNotFoundException(string message, Exception innerException);
    }
}