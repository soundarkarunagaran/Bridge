
namespace System.Collections.Generic
{
    [Bridge.External]
    [Bridge.Reflectable]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    public interface IReadOnlyCollection<T> : IEnumerable<T>
    {
        int Count
        {
            [Bridge.Template("System.Array.getCount({this}, {T})")]
            get;
        }
    }

    [Bridge.External]
    [Bridge.Reflectable]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    public interface IReadOnlyList<T> : IReadOnlyCollection<T>
    {
        T this[int index]
        {
            [Bridge.Template("System.Array.getItem({this}, {0}, {T})")]
            get;
        }
    }
}