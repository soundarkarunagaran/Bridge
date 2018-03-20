using System;

namespace Bridge
{
    /// <summary>
    /// Applies to interface if it's implementation is done outside Bridge type system (class implementation doesn't provide aliases for interface members implementations)
    /// </summary>
    [External]
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Interface)]
    [NonScriptable]
    public sealed class ExternalInterfaceAttribute : Attribute
    {
        public extern ExternalInterfaceAttribute();
        public extern ExternalInterfaceAttribute(bool nativeImplementation);

        public bool IsVirtual
        {
            get; set;
        }
    }
}