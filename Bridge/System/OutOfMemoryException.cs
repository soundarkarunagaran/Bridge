namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class OutOfMemoryException : SystemException, Bridge.IBridgeClass
    {
        public extern OutOfMemoryException();

        public extern OutOfMemoryException(string message);

        [Bridge.Template("new System.OutOfMemoryException({message}, {innerException})")]
        public extern OutOfMemoryException(string message, Exception innerException);
    }
}