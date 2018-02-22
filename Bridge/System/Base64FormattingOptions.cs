namespace System
{
    /// <summary>
    /// Specifies whether relevant "Convert.ToBase64CharArray" and "Convert.ToBase64String" methods insert line breaks in their output.
    /// </summary>
    [Bridge.External]
    [Bridge.Enum(Bridge.Emit.Value)]
    public enum Base64FormattingOptions
    {
        None = 0,
        InsertLineBreaks = 1,
    }
}