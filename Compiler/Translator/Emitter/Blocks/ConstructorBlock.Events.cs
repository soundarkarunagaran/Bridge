using Bridge.Contract;
using Bridge.Contract.Constants;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace Bridge.Translator
{
    public partial class ConstructorBlock
    {
        protected virtual IEnumerable<string> GetEventsAndAutoStartupMethods()
        {
            var methods = this.StaticBlock ? this.TypeInfo.StaticMethods : this.TypeInfo.InstanceMethods;
            List<string> list = new List<string>();

            bool hasReadyAttribute;
            var isGenericType = this.IsGenericType();

            foreach (var methodGroup in methods)
            {
                foreach (var declarationAndSymbol in methodGroup.Value)
                {
                    var method = declarationAndSymbol.Declaration;
                    var isGenericMethod = this.IsGenericMethod(method);

                    HandleAttributes(list, methodGroup, declarationAndSymbol, out hasReadyAttribute);

                    if (hasReadyAttribute)
                    {
                        if (isGenericType || isGenericMethod)
                        {
                            hasReadyAttribute = false;
                        }
                    }
                    else
                    {
                        if (this.StaticBlock && !hasReadyAttribute)
                        {
                            if (method.Name == CS.Methods.AUTO_STARTUP_METHOD_NAME
                                && method.HasModifier(Modifiers.Static)
                                && !method.HasModifier(Modifiers.Abstract)
                                && Helpers.IsEntryPointCandidate(this.Emitter, method))
                            {
                                if (isGenericType || isGenericMethod)
                                {
                                    LogAutoStartupWarning(method);
                                }
                            }
                        }
                    }

                    if (hasReadyAttribute)
                    {
                        this.HasEntryPoint = true;
                    }
                }
            }

            return list;
        }

        private void HandleAttributes(List<string> list, KeyValuePair<string, List<MethodDeclarationAndSymbol>> methodGroup, MethodDeclarationAndSymbol method, out bool hasReadyAttribute)
        {
            hasReadyAttribute = false;
            var isGenericType = this.IsGenericType();
            var isGenericMethod = this.IsGenericMethod(method.Declaration);

            if (method.Symbol.HasReadyAttribute())
            {
                hasReadyAttribute = true;

                if (isGenericType || isGenericMethod)
                {
                    LogAutoStartupWarning(method.Declaration);
                }
            }

            try
            {
                var adapters = method.Symbol.GetAdapters(methodGroup.Key, StaticBlock, Emitter);
                if (adapters.Count > 0)
                {
                    if (methodGroup.Value.Count > 1)
                    {
                        throw new EmitterException(method.Declaration.NameToken, "Overloaded method cannot be event handler");
                    }
                }
                list.AddRange(adapters);
            }
            catch (Exception e)
            {
                throw new EmitterException(method.Declaration.NameToken, e.Message);
            }
        }


        private void LogWarning(string message)
        {
            var logger = this.Emitter.Log as Bridge.Translator.Logging.Logger;
            bool? wrappingValue = null;

            if (logger != null && logger.UseTimeStamp)
            {
                wrappingValue = logger.UseTimeStamp;
                logger.UseTimeStamp = false;
            }

            this.Emitter.Log.Warn(message);

            if (wrappingValue.HasValue)
            {
                logger.UseTimeStamp = wrappingValue.Value;
            }
        }

        private void LogAutoStartupWarning(MethodDeclaration method)
        {
            this.LogWarning(string.Format("'{0}.{1}': an entry point cannot be generic or in a generic type", this.TypeInfo.Type.ReflectionName, method.Name));
        }
    }
}