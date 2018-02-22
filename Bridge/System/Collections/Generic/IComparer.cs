namespace System.Collections.Generic
{
    [Bridge.External]
    [Bridge.Reflectable]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    public interface IComparer<in T>
    {
        int Compare(T x, T y);
    }
}