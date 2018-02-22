namespace System.Reflection
{
    [Bridge.External]
    public class AmbiguousMatchException : Exception
    {
        public extern AmbiguousMatchException();

        public extern AmbiguousMatchException(string message);

        public extern AmbiguousMatchException(string message, Exception innerException);
    }
}