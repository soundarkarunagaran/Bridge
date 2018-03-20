using System;

namespace Bridge
{
    [External]
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Interface)]
    [NonScriptable]
    public sealed class FieldAttribute : Attribute
    {
    }
}