
namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Used to mark one or more entity properties that provide the entity's unique identity
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [Bridge.External]
    [Bridge.NonScriptable]
    public sealed class KeyAttribute : Attribute { }
}
