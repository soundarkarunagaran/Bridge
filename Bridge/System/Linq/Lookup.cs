using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
    [Bridge.External]
    [Bridge.IgnoreGeneric]
    [Bridge.Convention(Target = Bridge.ConventionTarget.Member, Member = Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    public interface ILookup<TKey, TElement> : IEnumerable<Grouping<TKey, TElement>>
    {
        int Count
        {
            [Bridge.Template("count()")]
            get;
        }

        [Bridge.AccessorsIndexer]
        EnumerableInstance<TElement> this[TKey key]
        {
            [Bridge.Template("get({0})")]
            get;
        }

        [Bridge.Template("contains({key})")]
        bool Contains(TKey key);
    }

    [Bridge.External]
    [Bridge.IgnoreGeneric]
    public class Lookup<TKey, TElement> : ILookup<TKey, TElement>
    {
        internal extern Lookup();

        public extern int Count
        {
            [Bridge.Template("count()")]
            get;
        }

        [Bridge.AccessorsIndexer]
        public extern EnumerableInstance<TElement> this[TKey key]
        {
            [Bridge.Template("get({0})")]
            get;
        }

        public extern bool Contains(TKey key);

        public extern IEnumerator<Grouping<TKey, TElement>> GetEnumerator();

        extern IEnumerator IEnumerable.GetEnumerator();
    }
}