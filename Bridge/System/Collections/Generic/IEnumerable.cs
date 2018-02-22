namespace System.Collections.Generic
{
    [Bridge.External]
    [Bridge.Reflectable]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    public interface IEnumerable<out T> : IEnumerable, Bridge.IBridgeClass
    {
        [Bridge.Template("Bridge.getEnumerator({this}, {T})")]
        new IEnumerator<T> GetEnumerator();
    }
}