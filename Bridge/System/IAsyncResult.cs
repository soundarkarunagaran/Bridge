using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
    [Bridge.External]
    [Bridge.Reflectable]
    public interface IAsyncResult
    {
        object AsyncState { get; }
        bool CompletedSynchronously { get; }
        bool IsCompleted { get; }
    }
}
