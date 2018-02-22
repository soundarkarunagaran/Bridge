namespace System.Collections
{
    [Bridge.External]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    [Bridge.Reflectable]
    public interface IEnumerable : Bridge.IBridgeClass
    {
        [Bridge.Template("Bridge.getEnumerator({this})")]
        IEnumerator GetEnumerator();
    }
}