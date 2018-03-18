using System;

namespace Bridge
{
    [External]
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    [NonScriptable]
    public sealed class CastAttribute : Attribute
    {
        public CastAttribute(string value)
        {
        }
    }
}