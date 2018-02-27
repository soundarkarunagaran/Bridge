using System.Reflection;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    public abstract class MemberBinding
    {
        [Bridge.Name("btype")]
        public extern MemberBindingType BindingType { get; private set; }

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern MemberInfo Member { get; private set; }

        internal extern MemberBinding();
    }
}