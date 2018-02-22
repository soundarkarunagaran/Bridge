namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class NotImplementedException : Exception, Bridge.IBridgeClass
    {
        public extern NotImplementedException();

        public extern NotImplementedException(string message);

        public extern NotImplementedException(string message, Exception innerException);
    }
}