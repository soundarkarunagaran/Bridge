namespace System.Diagnostics
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    [Bridge.External]
    [Bridge.NonScriptable]
    public sealed class ConditionalAttribute : Attribute
    {
        public extern ConditionalAttribute(string conditionString);

        public extern string ConditionString { get; }
    }
}