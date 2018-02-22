using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 6")]
    public sealed class MethodCallExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern MethodInfo Method { get; private set; }

        [Bridge.Name("obj")]
        public extern Expression Object { get; private set; }

        [Bridge.Name("args")]
        public extern ReadOnlyCollection<Expression> Arguments { get; private set; }

        internal extern MethodCallExpression();
    }
}