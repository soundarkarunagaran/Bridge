
namespace System
{
    [Bridge.External]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    [Bridge.Reflectable]
    public interface IDisposable
    {
        void Dispose();
    }
}