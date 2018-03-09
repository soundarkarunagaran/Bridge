using System.Threading;

namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class OperationCanceledException : Exception
    {
        public extern OperationCanceledException();

        [Bridge.Template("new System.OperationCanceledException(null, {token})")]
        public extern OperationCanceledException(CancellationToken token);

        [Bridge.Template("new System.OperationCanceledException({message}, System.Threading.CancellationToken.none)")]
        public extern OperationCanceledException(string message);

        [Bridge.Template("new System.OperationCanceledException({message}, System.Threading.CancellationToken.none, {innerException})")]
        public extern OperationCanceledException(string message, Exception innerException);

        public extern OperationCanceledException(string message, CancellationToken token);

        [Bridge.Template("new System.OperationCanceledException({message}, {token}, {innerException})")]
        public extern OperationCanceledException(string message, Exception innerException, CancellationToken token);

        [Bridge.Convention(Bridge.Notation.CamelCase)]
        public extern CancellationToken CancellationToken
        {
            get;
            private set;
        }
    }
}