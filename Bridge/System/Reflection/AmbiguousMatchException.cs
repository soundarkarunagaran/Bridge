namespace System.Reflection
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    public class AmbiguousMatchException : Exception
    {
        public extern AmbiguousMatchException();

        public extern AmbiguousMatchException(string message);

        public extern AmbiguousMatchException(string message, Exception innerException);
    }
}