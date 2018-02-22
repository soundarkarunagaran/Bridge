using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype == 50 && {this}.dtype === 1")]
    public sealed class DynamicInvocationExpression : DynamicExpression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern ReadOnlyCollection<Expression> Arguments { get; private set; }

        internal extern DynamicInvocationExpression();
    }
}