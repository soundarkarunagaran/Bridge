
namespace System
{
    [Bridge.External]
    [Bridge.NonScriptable]
    public interface IServiceProvider
    {
        Object GetService(Type serviceType);
    }
}