using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
    [Bridge.External]
    //[Bridge.Name("System.Threading.Tasks.Task")]
    public class TaskAwaiter : INotifyCompletion
    {
        internal extern TaskAwaiter();

        public extern bool IsCompleted
        {
            [Bridge.Template("isCompleted()")]
            get;
        }

        [Bridge.Name("continueWith")]
        public extern void OnCompleted(Action continuation);

        [Bridge.Name("getAwaitedResult")]
        public extern void GetResult();
    }

    [Bridge.External]
    [Bridge.Name("System.Threading.Tasks.Task")]
    public class TaskAwaiter<TResult> : INotifyCompletion
    {
        internal extern TaskAwaiter();

        public extern bool IsCompleted
        {
            [Bridge.Template("isCompleted()")]
            get;
        }

        [Bridge.Name("continueWith")]
        public extern void OnCompleted(Action continuation);

        [Bridge.Name("getAwaitedResult")]
        public extern TResult GetResult();
    }
}