namespace System.Diagnostics
{
    [Bridge.External]
    [Bridge.Name("System.Object")]
    public static class Debugger
    {
        [Bridge.Template("debugger")]
        public static extern void Break();
    }
}