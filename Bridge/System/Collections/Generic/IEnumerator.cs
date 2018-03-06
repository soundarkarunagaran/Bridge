namespace System.Collections.Generic
{
    [Bridge.External]
    [Bridge.Reflectable]
    [Bridge.ExternalInterface]
    public interface IEnumerator<out T> : Bridge.IBridgeClass, IDisposable, IEnumerator
    {
        new T Current
        {
            get;
        }
    }
}