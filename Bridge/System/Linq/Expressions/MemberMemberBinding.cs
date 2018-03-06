using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.btype === 1")]
    public sealed class MemberMemberBinding : MemberBinding
    {
        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern ReadOnlyCollection<MemberBinding> Bindings { get; private set; }

        internal extern MemberMemberBinding();
    }
}