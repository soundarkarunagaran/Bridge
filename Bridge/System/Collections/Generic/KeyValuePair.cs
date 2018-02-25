namespace System.Collections.Generic
{
    [Bridge.External]
    [Bridge.Immutable]
    [Bridge.Reflectable]
    public struct KeyValuePair<TKey, TValue> : Bridge.IBridgeClass
    {
        public extern KeyValuePair(TKey key, TValue value);

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern TKey Key
        {
            get;
        }

        [Bridge.Convention(Bridge.Notation.CamelCase)] //[Field]
        public extern TValue Value
        {
            get;
        }
    }
}