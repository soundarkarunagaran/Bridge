using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 18")]
    public abstract class LambdaExpression : Expression
    {
        [Bridge.Name("p")]
        public extern ReadOnlyCollection<ParameterExpression> Parameters { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Body { get; private set; }

        [Bridge.Name("rt")]
        public extern Expression ReturnType { get; private set; }

        internal extern LambdaExpression();
    }
}