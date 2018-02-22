namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 53")]
    public sealed class GotoExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern GotoExpressionKind Kind { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Value { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern LabelTarget Target { get; private set; }

        internal extern GotoExpression();
    }
}