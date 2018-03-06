namespace System.Diagnostics.CodeAnalysis
{
    [Bridge.Convention(Member = Bridge.ConventionMember.Field | Bridge.ConventionMember.Method, Notation = Bridge.Notation.CamelCase)]
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    [Bridge.External]
    [Bridge.Name("System.Object")]
    public sealed class SuppressMessageAttribute : Attribute
    {
        public extern SuppressMessageAttribute(string category, string checkId);

        public extern string Category { get; }

        public extern string CheckId { get; }

        public extern string Justification { get; set; }

        public extern string Scope { get; set; }

        public extern string Target { get; set; }

        public extern string MessageId { get; set; }
    }
}