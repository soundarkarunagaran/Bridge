namespace System.Threading
{
    [Bridge.External]
    [Bridge.Reflectable]
    public struct CancellationTokenRegistration : IEquatable<CancellationTokenRegistration>, IDisposable
    {
        public extern bool Equals(CancellationTokenRegistration other);

        public extern void Dispose();

        [Bridge.Template("Bridge.equals({left}, {right})")]
        public static extern bool operator ==(CancellationTokenRegistration left, CancellationTokenRegistration right);

        [Bridge.Template("!Bridge.equals({left}, {right})")]
        public static extern bool operator !=(CancellationTokenRegistration left, CancellationTokenRegistration right);
    }
}