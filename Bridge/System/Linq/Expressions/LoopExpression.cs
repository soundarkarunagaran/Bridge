namespace System.Linq.Expressions
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 58")]
    public sealed class LoopExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern Expression Body { get; private set; }

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern LabelTarget BreakLabel { get; private set; }

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern LabelTarget ContinueLabel { get; private set; }

        internal extern LoopExpression();
    }
}