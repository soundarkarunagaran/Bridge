using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 17")]
    public sealed class InvocationExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Expression { get; private set; }

        [Bridge.Name("args")]
        public extern ReadOnlyCollection<Expression> Arguments { get; private set; }

        internal extern InvocationExpression();
    }
}