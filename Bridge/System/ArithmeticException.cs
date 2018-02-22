
namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class ArithmeticException : Exception, Bridge.IBridgeClass
    {
        public extern ArithmeticException();

        public extern ArithmeticException(string message);

        public extern ArithmeticException(string message, Exception innerException);
    }
}