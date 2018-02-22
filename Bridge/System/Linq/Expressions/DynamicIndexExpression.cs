namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype == 50 && {this}.dtype === 2")]
    public sealed class DynamicIndexExpression : DynamicExpression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Argument { get; private set; }

        internal extern DynamicIndexExpression();
    }
}