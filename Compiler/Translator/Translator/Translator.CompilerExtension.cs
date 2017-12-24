using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bridge.Translator
{
    public partial class Translator
    {
        private void ExecuteCompilerExtensions()
        {
            var appDomain = AppDomain.CreateDomain("BridgeCompilerExtension");
            try
            {
                // first load real mscorlib
                appDomain.Load(typeof(string).Assembly.GetName());

                // load placeholder for bridge types

                // register type and assembly resolver 
                appDomain.TypeResolve += (sender, args) =>
                {
                    Log.Trace($"Resolving type {args.Name}");
                    return null;
                };
                appDomain.AssemblyResolve += (sender, args) =>
                {
                    Log.Trace($"Resolving assembly {args.Name}");
                    return appDomain.Load(args.Name);
                };

                // try to load output assembly (we load it as binary copy to not block the output assembly)
                var outputAssembly = appDomain.Load(File.ReadAllBytes(this.AssemblyLocation));

                // find all compiler extensions 
                var types = outputAssembly.ExportedTypes.Where(t => t.IsAbstract && t.IsSealed);
                foreach (var type in types)
                {
                    type.GetMethods(BindingFlags.Public | BindingFlags.Static).Where(m=>m.GetCustomAttributes)
                }



            }
            catch (Exception e)
            {
                Log.Error($"Failed to execute compiler extensions: {e}");
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }
    }
}