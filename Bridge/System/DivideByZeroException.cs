
namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class DivideByZeroException : ArithmeticException, Bridge.IBridgeClass
    {
        public extern DivideByZeroException();

        public extern DivideByZeroException(string message);

        public extern DivideByZeroException(string message, Exception innerException);
    }
}