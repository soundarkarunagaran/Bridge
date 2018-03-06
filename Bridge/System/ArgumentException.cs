namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class ArgumentException : Exception, Bridge.IBridgeClass
    {
        public extern ArgumentException();

        public extern ArgumentException(string message);

        [Bridge.Template("new System.ArgumentException({message}, null, {innerException})")]
        public extern ArgumentException(string message, Exception innerException);

        public extern ArgumentException(string message, string paramName);

        public extern ArgumentException(string message, string paramName, Exception innerException);

        /// <summary>
        /// Gets the name of the parameter that causes this exception.
        /// </summary>
        public extern string ParamName
        {
            get;
        }
    }
}