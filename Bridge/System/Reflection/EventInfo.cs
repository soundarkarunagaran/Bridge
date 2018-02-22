namespace System.Reflection
{
    [Bridge.External]
    public class EventInfo : MemberInfo
    {
        [Bridge.Name("ad")]
        public extern MethodInfo AddMethod
        {
            get;
            private set;
        }

        [Bridge.Name("r")]
        public extern MethodInfo RemoveMethod
        {
            get;
            private set;
        }

        [Bridge.Template("Bridge.Reflection.midel({this}.ad, {target})({handler})")]
        public extern void AddEventHandler(object target, Delegate handler);

        [Bridge.Template("Bridge.Reflection.midel({this}.r, {target})({handler})")]
        public extern void RemoveEventHandler(object target, Delegate handler);

        internal extern EventInfo();
    }
}