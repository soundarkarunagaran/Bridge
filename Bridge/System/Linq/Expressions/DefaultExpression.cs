
namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 51")]
    public sealed class DefaultExpression : Expression
    {
        internal extern DefaultExpression();
    }
}