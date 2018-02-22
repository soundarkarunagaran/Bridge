using System.Reflection;

namespace System
{
    [Bridge.External]
    public sealed class AppDomain
    {
        private extern AppDomain();

        public extern Assembly[] GetAssemblies();

        public static extern AppDomain CurrentDomain
        {
            [Bridge.Template("System.AppDomain")]
            get;
        }
    }
}