namespace System.Runtime.CompilerServices
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    public static class FormattableStringFactory
    {
        public static FormattableString Create(string format, params object[] args)
        {
            return new FormattableStringImpl(format, args);
        }
    }
}
