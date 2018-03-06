namespace System.Diagnostics
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [Bridge.External]
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static extern void Assert(bool condition);

        [Conditional("DEBUG")]
        public static extern void Assert(bool condition, string message);

        [Conditional("DEBUG")]
        public static extern void Fail(string message);

        [Conditional("DEBUG")]
        [Bridge.Name("writeln")]
        public static extern void WriteLine(string message);

        [Conditional("DEBUG")]
        [Bridge.Template("debugger")]
        public static extern void Break();
    }
}