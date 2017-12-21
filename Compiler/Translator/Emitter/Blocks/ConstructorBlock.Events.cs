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

            var adapters = method.Symbol.GetAdapterAttributes();

            foreach (var attr in adapters)
            {
                if (methodGroup.Value.Count > 1)
                {
                    // TODO: maybe try to load correct adapter attribute?
                    throw new EmitterException(method.Declaration.NameToken, "Overloaded method cannot be event handler");
                }


                // If the attribute defines a 'public const bool StaticOnly = true' the attribute can only be applied to static methods
                var staticFlagField = attr.AttributeType.GetFields(f => f.Name == "StaticOnly").FirstOrDefault();
                if (staticFlagField != null)
                {
                    var staticValue = staticFlagField.ConstantValue;

                    if (staticValue is bool && ((bool)staticValue) && !method.Symbol.IsStatic)
                    {
                        throw new EmitterException(method.Declaration.NameToken, attr.AttributeType.FullName + " can be applied for static methods only");
                    }
                }

                // if the adapter defines a 'public const string Event = "{something}"'
                // this becomes the event name
                string eventName = methodGroup.Key;
                var eventField = attr.AttributeType.GetFields(f => f.Name == "Event");
                if (eventField.Any())
                {
                    eventName = eventField.First().ConstantValue.ToString();
                }

                // The adapter can define a string-format on how the event is registered
                string format = null;
                string formatName = this.StaticBlock ? "Format" : "FormatScope";
                var formatField = attr.AttributeType.GetFields(f => f.Name == formatName, GetMemberOptions.IgnoreInheritedMembers);
                if (formatField.Any())
                {
                    format = formatField.First().ConstantValue.ToString();
                }
                else
                {
                    var baseTypes = attr.AttributeType.GetAllBaseTypes().ToArray();
                    for (int i = baseTypes.Length - 1; i >= 0; i--)
                    {
                        formatField = baseTypes[i].GetFields(f => f.Name == formatName);

                        if (formatField.Any())
                        {
                            format = formatField.First().ConstantValue.ToString();
                            break;
                        }
                    }
                }

                // If an attribute defines 'public const bool IsCommonEvent=true' 
                // then the first parameter of the attribute constructor defines the name of the event
                bool isCommon = false;
                var commonField = attr.AttributeType.GetFields(f => f.Name == "IsCommonEvent");
                if (commonField.Count() > 0)
                {
                    isCommon = Convert.ToBoolean(commonField.First().ConstantValue);
                    if (isCommon)
                    {
                        var commonEventName = GetEventNameFromAttributeParameter(attr.PositionalArguments[0]);
                        if (commonEventName != null)
                        {
                            eventName = commonEventName;
                        }
                    }
                }

                // the next parameter is the selector to which the event is subscribed
                // the selector is a string-format template which is filled with value defiend by the last parameter
                var selectorIndex = isCommon ? 1 : 0;
                var selector = (attr.PositionalArguments.ElementAt(selectorIndex)).ConstantValue.ToString();

                // find the matchine enum value
                var type = attr.PositionalArguments[selectorIndex + 1].Type;
                var fields = type.GetFields(f =>
                {
                    var field = f as DefaultResolvedField;

                    if (field != null && field.ConstantValue != null && Convert.ToInt32(field.ConstantValue.ToString()) == Convert.ToInt32(attr.PositionalArguments[0].ConstantValue))
                    {
                        return true;
                    }

                    var field1 = f as DefaultUnresolvedField;

                    if (field1 != null && field1.ConstantValue != null && Convert.ToInt32(field1.ConstantValue.ToString()) == Convert.ToInt32(attr.PositionalArguments[0].ConstantValue))
                    {
                        return true;
                    }

                    return false;
                }, GetMemberOptions.IgnoreInheritedMembers).FirstOrDefault();

                if (fields != null)
                {
                    // load the template from this enum field
                    var template = fields.GetTemplate(Emitter);
                    if (template != null)
                    {
                        selector = string.Format(template, selector);
                    }
                }

                list.Add(string.Format(format, eventName, selector, this.Emitter.GetEntityName(method.Symbol)));
            }
        }

        private string GetEventNameFromAttributeParameter(ResolveResult argument)
        {
            // for string parameters we directly accept the name
            if (argument.ConstantValue is string)
            {
                return argument.ConstantValue.ToString();
            }

            // another possibilitiy is having an enum, in this case we need to find the 
            // matching enum field and we will use the field name 
            var type = argument.Type;
            var fields = type.GetFields(f =>
            {
                var field = f as DefaultResolvedField;

                if (field != null && field.ConstantValue != null && Convert.ToInt32(field.ConstantValue.ToString()) == Convert.ToInt32(argument.ConstantValue))
                {
                    return true;
                }

                var field1 = f as DefaultUnresolvedField;

                if (field1 != null && field1.ConstantValue != null && Convert.ToInt32(field1.ConstantValue.ToString()) == Convert.ToInt32(argument.ConstantValue))
                {
                    return true;
                }

                return false;
            }, GetMemberOptions.IgnoreInheritedMembers).FirstOrDefault();

            if (fields != null)
            {
                return this.Emitter.GetEntityName(fields);
            }

            return null;
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