namespace System.Threading.Tasks
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Reflectable]
    public class TaskCanceledException : OperationCanceledException
    {
        public extern TaskCanceledException();

        public extern TaskCanceledException(string message);

        [Bridge.Template("new System.Threading.Tasks.TaskCanceledException(null, {task})")]
        public extern TaskCanceledException(Task task);

        [Bridge.Template("new System.Threading.Tasks.TaskCanceledException({message}, null, {innerException})")]
        public extern TaskCanceledException(string message, Exception innerException);

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern Task Task
        {
            get;
            private set;
        }
    }
}