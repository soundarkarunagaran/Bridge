
namespace System.ComponentModel.DataAnnotations.Schema
{
    /// <summary>
    /// Denotes that a property or class should be excluded from database mapping.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false)]
    [Bridge.External]
    [Bridge.NonScriptable]
    public class NotMappedAttribute : Attribute { }
}
