using System;

namespace Bridge
{
    [External]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate | AttributeTargets.Interface, AllowMultiple = true)]
    [NonScriptable]
    public sealed class IgnoreCastAttribute : Attribute
    {
    }
}