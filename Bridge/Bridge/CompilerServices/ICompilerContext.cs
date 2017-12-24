namespace Bridge.CompilerServices
{
    [ExternalInterface]
    [NonScriptable]
    public interface ICompilerContext
    {
        IAttributesContext Attributes { get; }
    }
}
