namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class ArithmeticException : Exception, Bridge.IBridgeClass
    {
        public extern ArithmeticException();

        public extern ArithmeticException(string message);

        public extern ArithmeticException(string message, Exception innerException);
    }
}