namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class OverflowException : ArithmeticException, Bridge.IBridgeClass
    {
        public extern OverflowException();

        public extern OverflowException(string message);

        public extern OverflowException(string message, Exception innerException);
    }
}