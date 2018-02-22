namespace System.Threading
{
    [Bridge.External]
    [Bridge.Reflectable]
    public struct CancellationToken
    {
        public extern CancellationToken(bool canceled);

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public static extern CancellationToken None
        {
            get;
        }

        public extern bool CanBeCanceled
        {
            [Bridge.Template("getCanBeCanceled()")]
            get;
        }

        public extern bool IsCancellationRequested
        {
            [Bridge.Template("getIsCancellationRequested()")]
            get;
        }

        public extern void ThrowIfCancellationRequested();

        public extern CancellationTokenRegistration Register(Action callback);

        [Bridge.Template("{this}.register({callback})")]
        public extern CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext);

        public extern CancellationTokenRegistration Register(Action<object> callback, object state);

        [Bridge.Template("{this}.register({callback}, {state})")]
        public extern CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext);
    }
}