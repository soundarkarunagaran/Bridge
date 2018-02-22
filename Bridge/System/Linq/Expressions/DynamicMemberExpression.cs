
namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype == 50 && {this}.dtype === 0")]
    public sealed class DynamicMemberExpression : DynamicExpression
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern string Member { get; private set; }

        internal extern DynamicMemberExpression();
    }
}