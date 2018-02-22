
namespace System.Collections
{
    [Bridge.External]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    [Bridge.Reflectable]
    public interface IEnumerator : Bridge.IBridgeClass
    {
        object Current
        {
            get;
        }

        bool MoveNext();

        void Reset();
    }
}