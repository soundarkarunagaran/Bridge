namespace System.Runtime.CompilerServices
{
    [Bridge.NonScriptable]
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class CallerLineNumberAttribute : Attribute
    {
    }

    [Bridge.NonScriptable]
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class CallerFilePathAttribute : Attribute
    {
    }

    [Bridge.NonScriptable]
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public class CallerMemberNameAttribute : Attribute
    {
    }
}