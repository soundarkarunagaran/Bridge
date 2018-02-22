
namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class OverflowException : ArithmeticException, Bridge.IBridgeClass
    {
        public extern OverflowException();

        public extern OverflowException(string message);

        public extern OverflowException(string message, Exception innerException);
    }
}