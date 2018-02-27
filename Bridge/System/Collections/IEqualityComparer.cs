namespace System.Collections
{
    [Bridge.External]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.Reflectable]
    public interface IEqualityComparer : Bridge.IBridgeClass
    {
        bool Equals(object x, object y);

        int GetHashCode(object obj);
    }
}