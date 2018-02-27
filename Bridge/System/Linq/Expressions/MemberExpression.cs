using System.Reflection;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 23")]
    public sealed class MemberExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern MemberInfo Member { get; private set; }

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern Expression Expression { get; private set; }

        internal extern MemberExpression();
    }
}