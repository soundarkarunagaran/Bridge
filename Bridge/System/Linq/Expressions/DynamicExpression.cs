
namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype == 50")]
    public abstract class DynamicExpression : Expression
    {
        [Bridge.Name("dtype")]
        public extern DynamicExpressionType DynamicType { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Expression { get; private set; }

        internal extern DynamicExpression();
    }
}