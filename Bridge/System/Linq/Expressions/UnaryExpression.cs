using System.Reflection;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("[4,10,11,28,29,30,34,40,44,49,54,60,62,77,78,79,80,82,83,84].indexOf({this}.ntype) >= 0")]
    public sealed class UnaryExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Operand { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern MethodInfo Method { get; private set; }

        internal extern UnaryExpression();
    }
}