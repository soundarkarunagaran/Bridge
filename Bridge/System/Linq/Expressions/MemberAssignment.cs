
namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.btype === 0")]
    public sealed class MemberAssignment : MemberBinding
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Expression { get; private set; }

        internal extern MemberAssignment();
    }
}