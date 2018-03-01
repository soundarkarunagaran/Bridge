using Bridge;

namespace System.Collections.Generic
{
    [External]
    [Reflectable]
    [Convention(Target = ConventionTarget.Member, Member = ConventionMember.Method, Notation = Notation.LowerCamelCase)]
    public interface IReadOnlyCollection<out T> : IEnumerable<T>
    {
        int Count
        {
            [Template("System.Array.getCount({this}, {T})")]
            get;
        }
    }
}