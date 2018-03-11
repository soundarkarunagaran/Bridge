using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// Should compile, but be ignored as volatile is not relevant in the JavaScript world.
    /// </summary>
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [System.Runtime.InteropServices.ComVisible(true)]
    [Bridge.External]
    public static class IsVolatile
    {
    }
}