namespace System.Collections.Generic
{
    [Bridge.External]
    [Bridge.Reflectable]
    [Bridge.ExternalInterface]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    public interface IEnumerator<out T> : Bridge.IBridgeClass, IDisposable, IEnumerator
    {
        new T Current
        {
            get;
        }
    }
}