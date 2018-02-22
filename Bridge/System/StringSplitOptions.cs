
namespace System
{
    [Bridge.External]
    [Bridge.Name("Number")]
    [Bridge.Enum(Bridge.Emit.Value)]
    public enum StringSplitOptions
    {
        None = 0,
        RemoveEmptyEntries = 1
    }
}