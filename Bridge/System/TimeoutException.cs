namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class TimeoutException : SystemException
    {
        public extern TimeoutException();

        public extern TimeoutException(string message);

        [Bridge.Template("new System.TimeoutException({message}, {innerException})")]
        public extern TimeoutException(string message, Exception innerException);
    }
}