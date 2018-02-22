
namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    public sealed class CatchBlock
    {
        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern ParameterExpression Variable { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Type Test { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Body { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern Expression Filter { get; private set; }

        internal extern CatchBlock();
    }
}