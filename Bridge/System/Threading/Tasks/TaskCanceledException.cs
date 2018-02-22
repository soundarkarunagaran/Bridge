
namespace System.Threading.Tasks
{
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

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Task Task
        {
            get;
            private set;
        }
    }
}