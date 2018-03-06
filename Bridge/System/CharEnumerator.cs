using System.Collections;
using System.Collections.Generic;

namespace System
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    public sealed class CharEnumerator : IEnumerator, /*ICloneable,*/ IEnumerator<char>, IDisposable
    {
        extern object IEnumerator.Current
        {
            get;
        }

        public extern char Current
        {
            get;
        }

        internal extern CharEnumerator(string str);

        public extern bool MoveNext();

        public extern void Dispose();

        public extern void Reset();
    }
}