using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    [Bridge.Cast("{this}.ntype === 55")]
    public sealed class IndexExpression : Expression
    {
        [Bridge.Name("obj")]
        public extern Expression Object { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern PropertyInfo Indexer { get; private set; }

        [Bridge.Convention(Bridge.Notation.LowerCamelCase)] //[Field]
        public extern ReadOnlyCollection<Expression> Arguments { get; private set; }

        internal extern IndexExpression();
    }
}