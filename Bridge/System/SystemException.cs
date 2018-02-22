
namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class SystemException : Exception, Bridge.IBridgeClass
    {
        public extern SystemException();

        public extern SystemException(string message);

        [Bridge.Template("new System.SystemException({message}, {innerException})")]
        public extern SystemException(string message, Exception innerException);
    }
}