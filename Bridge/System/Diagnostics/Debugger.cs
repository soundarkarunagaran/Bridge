namespace System.Diagnostics
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    [Bridge.Name("System.Object")]
    public static class Debugger
    {
        [Bridge.Template("debugger")]
        public static extern void Break();
    }
}