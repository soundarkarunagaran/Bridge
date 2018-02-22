
namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class NotSupportedException : Exception, Bridge.IBridgeClass
    {
        public extern NotSupportedException();

        public extern NotSupportedException(string message);

        public extern NotSupportedException(string message, Exception innerException);
    }
}