
namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public class ArgumentNullException : ArgumentException, Bridge.IBridgeClass
    {
        public extern ArgumentNullException();

        public extern ArgumentNullException(string paramName);

        public extern ArgumentNullException(string paramName, string message);

        [Bridge.Template("new System.ArgumentNullException(null, {message}, {innerException})")]
        public extern ArgumentNullException(string message, Exception innerException);
    }
}