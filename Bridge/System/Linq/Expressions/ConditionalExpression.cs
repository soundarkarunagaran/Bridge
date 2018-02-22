
namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 8")]
    public sealed class ConditionalExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Test { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression IfTrue { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression IfFalse { get; private set; }

        internal extern ConditionalExpression();
    }
}