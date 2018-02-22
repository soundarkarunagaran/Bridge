namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 9")]
    public sealed class ConstantExpression : Expression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern object Value { get; private set; }

        internal extern ConstantExpression();
    }
}