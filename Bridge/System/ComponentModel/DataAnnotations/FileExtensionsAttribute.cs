
namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    [Bridge.External]
    [Bridge.NonScriptable]
    public sealed class FileExtensionsAttribute : DataTypeAttribute
    {
        public extern FileExtensionsAttribute();

        public extern string Extensions { get; set; }
    }
}
