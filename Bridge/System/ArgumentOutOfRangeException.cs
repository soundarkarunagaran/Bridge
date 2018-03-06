namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class ArgumentOutOfRangeException : ArgumentException, Bridge.IBridgeClass
    {
        public extern ArgumentOutOfRangeException();

        public extern ArgumentOutOfRangeException(string paramName);

        [Bridge.Template("new System.ArgumentOutOfRangeException(null, {message}, {innerException})")]
        public extern ArgumentOutOfRangeException(string message, Exception innerException);

        public extern ArgumentOutOfRangeException(string paramName, string message);

        [Bridge.Template("new System.ArgumentOutOfRangeException({paramName}, {message}, null, {actualValue})")]
        public extern ArgumentOutOfRangeException(string paramName, object actualValue, string message);

        public extern object ActualValue
        {
            get;
        }
    }
}