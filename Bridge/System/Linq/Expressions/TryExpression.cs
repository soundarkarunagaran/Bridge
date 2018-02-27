using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 61")]
    public sealed class TryExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern Expression Body { get; private set; }

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern ReadOnlyCollection<CatchBlock> Handlers { get; private set; }

        [Bridge.Name("finallyExpr")]
        public extern Expression Finally { get; private set; }

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern Expression Fault { get; private set; }

        internal extern TryExpression();
    }
}