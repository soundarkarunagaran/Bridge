using System.Collections.Generic;

namespace System.Linq
{
    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.LowerCamelCase)]
    public interface IGrouping<out TKey, out TElement> : IEnumerable<TElement>
    {
        TKey Key
        {
            [Bridge.Template("key()")]
            get;
        }
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    public class Grouping<TKey, TElement> : EnumerableInstance<TElement>, IGrouping<TKey, TElement>
    {
        internal extern Grouping();

        public extern TKey Key
        {
            [Bridge.Template("key()")]
            get;
        }
    }
}