using System;

namespace Bridge
{
    [External]
    [AttributeUsage(AttributeTargets.Method)]
    [NonScriptable]
    public sealed class CompilerExtensionAttribute : Attribute
    {
    }
}
