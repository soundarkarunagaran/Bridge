
namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 38")]
    public sealed class ParameterExpression : Expression
    {
        [Bridge.Name("n")]
        public extern string Name { get; private set; }

        internal extern ParameterExpression();
    }
}