namespace Bridge.CompilerServices
{
#if CORE
    [External]
    [NonScriptable]
#endif
    public enum AttributeTarget
    {
        Default,
        ReturnValue,
        Parameter,
        Getter,
        Setter,
        Adder,
        Remover
    }
}