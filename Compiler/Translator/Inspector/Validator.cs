using Bridge.Contract;
using Bridge.Contract.Constants;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.TypeSystem.Implementation;
using Mono.Cecil;
using Object.Net.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Mono.Cecil.Rocks;
using ICustomAttributeProvider = Mono.Cecil.ICustomAttributeProvider;

namespace Bridge.Translator
{
    public class Validator : IValidator
    {
        public virtual bool CanIgnoreType(TypeDefinition type)
        {
            if (type.IsExternal())
            {
                return true;
            }

            if (type.BaseType != null && type.BaseType.FullName == "System.MulticastDelegate")
            {
                return true;
            }

            return false;
        }

        public virtual void CheckType(TypeDefinition type, ITranslator translator)
        {
            this.CheckObjectLiteral(type, translator);

            if (this.CanIgnoreType(type))
            {
                return;
            }

            this.CheckConstructors(type, translator);
            this.CheckFields(type, translator);
            this.CheckProperties(type, translator);
            this.CheckMethods(type, translator);
            this.CheckEvents(type, translator);
            this.CheckFileName(type, translator);
            this.CheckModule(type, translator);
            this.CheckModuleDependenies(type, translator);
        }

        public virtual void CheckObjectLiteral(TypeDefinition type, ITranslator translator)
        {
            if (!type.IsObjectLiteral())
            {
                return;
            }

            var objectCreateMode = type.GetObjectCreateMode();

            if (objectCreateMode == 0)
            {
                var ctors = type.GetConstructors();

                foreach (var ctor in ctors)
                {
                    foreach (var parameter in ctor.Parameters)
                    {
                        if (parameter.ParameterType.FullName == "Bridge.ObjectCreateMode")
                        {
                            TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_PLAIN_NO_CREATE_MODE_CUSTOM_CONSTRUCTOR, type);
                        }

                        if (parameter.ParameterType.FullName == "Bridge.ObjectInitializationMode")
                        {
                            continue;
                        }

                        TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_PLAIN_CUSTOM_CONSTRUCTOR, type);
                    }
                }
            }

            if (type.IsInterface)
            {
                if (type.HasMethods && type.Methods.GroupBy(m => m.Name).Any(g => g.Count() > 1))
                {
                    TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_INTERFACE_NO_OVERLOAD_METHODS, type);
                }

                if (type.HasEvents)
                {
                    TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_INTERFACE_NO_EVENTS, type);
                }
            }
            else
            {
                if (type.Methods.Any(m => !m.IsRuntimeSpecialName && m.Name.Contains(".") && !m.Name.Contains("<")) ||
                    type.Properties.Any(m => !m.IsRuntimeSpecialName && m.Name.Contains(".") && !m.Name.Contains("<")))
                {
                    TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_INTERFACE_NO_EXPLICIT_IMPLEMENTATION, type);
                }
            }

            if (type.BaseType != null)
            {
                TypeDefinition baseType = null;
                try
                {
                    baseType = type.BaseType.Resolve();
                }
                catch (Exception)
                {

                }

                if (objectCreateMode == 1 && baseType != null && baseType.FullName != "System.Object" && baseType.FullName != "System.ValueType" && baseType.GetObjectCreateMode() == 0)
                {
                    TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_CONSTRUCTOR_INHERITANCE, type);
                }

                if (objectCreateMode == 0 && baseType != null && baseType.GetObjectCreateMode() == 1)
                {
                    TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_PLAIN_INHERITANCE, type);
                }
            }

            if (type.Interfaces.Count > 0)
            {
                foreach (var @interface in type.Interfaces)
                {
                    TypeDefinition iDef = null;
                    try
                    {
                        iDef = @interface.Resolve();
                    }
                    catch (Exception)
                    {

                    }

                    if (iDef != null && iDef.FullName != "System.Object" && !iDef.IsObjectLiteral())
                    {
                        TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_INTERFACE_INHERITANCE, type);
                    }
                }
            }

            if (objectCreateMode == 0)
            {
                var hasVirtualMethods = false;

                foreach (MethodDefinition method in type.Methods)
                {
                    if (method.IsCompilerGenerated())
                    {
                        continue;
                    }

                    if (method.IsVirtual && !(method.IsSetter || method.IsGetter))
                    {
                        hasVirtualMethods = true;
                        break;
                    }
                }

                if (hasVirtualMethods)
                {
                    Bridge.Translator.TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_NO_VIRTUAL_METHODS, type);
                }
            }
        }

        public virtual bool IsBridgeClass(TypeDefinition type)
        {
            foreach (var i in type.Interfaces)
            {
                if (i.FullName == JS.Types.BRIDGE_IBridgeClass)
                {
                    return true;
                }
            }

            return false;
        }

        private Stack<TypeDefinition> _stack = new Stack<TypeDefinition>();

        public virtual string GetCustomTypeName(TypeDefinition type, IEmitter emitter, bool excludeNs)
        {
            if (this._stack.Contains(type))
            {
                return null;
            }

            var nsAtrr = excludeNs ? null : type.GetNamespace();
            bool hasNs = nsAtrr != null;
            var nameAttr = type.GetName();

            string name = null;
            bool changeCase = false;
            if (nameAttr != null)
            {
                if (nameAttr.Item1 != null)
                {
                    name = Helpers.ConvertNameTokens(nameAttr.Item1, type.Name);
                }
                else 
                {
                    var boolValue = (bool)nameAttr.Item2;

                    if (boolValue)
                    {
                        if (hasNs)
                        {
                            changeCase = true;
                        }
                        else
                        {
                            this._stack.Push(type);
                            name = BridgeTypes.ToJsName(type, emitter);
                            var i = name.LastIndexOf(".");

                            if (i > -1)
                            {
                                char[] chars = name.ToCharArray();
                                chars[i + 1] = Char.ToLowerInvariant(chars[i + 1]);
                                name = new string(chars);
                            }
                            else
                            {
                                name = name.ToLowerCamelCase();
                            }
                            this._stack.Pop();

                            return name;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }

            if (hasNs)
            {
                name = "";
                if (nsAtrr.Item1 != null)
                {
                    name = nsAtrr.Item1;
                }

                if (nsAtrr.Item2)
                {
                    return null;
                }

                if (type.IsNested)
                {
                    name = (string.IsNullOrEmpty(name) ? "" : (name + ".")) + BridgeTypes.GetParentNames(emitter, type);
                }


                var typeName = emitter.GetTypeName(emitter.BridgeTypes.Get(type).Type.GetDefinition(), type);
                name = (string.IsNullOrEmpty(name) ? "" : (name + ".")) + BridgeTypes.ConvertName(changeCase ? typeName.ToLowerCamelCase() : typeName);

                return name;
            }

            if (type.IsObjectLiteral())
            {
                var mode = type.GetObjectCreateMode();
                if (type.IsExternal() && mode == 0)
                {
                    return JS.Types.System.Object.NAME;
                }
            }

            return null;
        }

        public virtual void CheckConstructors(TypeDefinition type, ITranslator translator)
        {
            if (type.HasMethods)
            {
                var ctors = type.Methods.Where(method => method.IsConstructor);

                foreach (MethodDefinition ctor in ctors)
                {
                    this.CheckMethodArguments(ctor);
                }
            }
        }

        public virtual void CheckFields(TypeDefinition type, ITranslator translator)
        {

        }

        public virtual void CheckProperties(TypeDefinition type, ITranslator translator)
        {
            /*if (type.HasProperties && this.IsObjectLiteral(type))
            {
                var objectCreateMode = this.GetObjectCreateMode(type);
                if (objectCreateMode == 0)
                {
                    foreach (PropertyDefinition prop in type.Properties)
                    {
                        if ((prop.GetMethod != null && prop.GetMethod.IsVirtual) || (prop.SetMethod != null && prop.SetMethod.IsVirtual))
                        {
                            TranslatorException.Throw(Constants.Messages.Exceptions.OBJECT_LITERAL_NO_VIRTUAL_METHODS, type);
                        }
                    }
                }
            }*/
        }

        public virtual void CheckEvents(TypeDefinition type, ITranslator translator)
        {
        }

        public virtual void CheckMethods(TypeDefinition type, ITranslator translator)
        {
            foreach (MethodDefinition method in type.Methods)
            {
                if (method.IsCompilerGenerated())
                {
                    continue;
                }

                this.CheckMethodArguments(method);
            }
        }

        public virtual void CheckMethodArguments(MethodDefinition method)
        {
        }

        public virtual HashSet<string> GetParentTypes(IDictionary<string, TypeDefinition> allTypes)
        {
            var result = new HashSet<string>();

            foreach (var type in allTypes.Values)
            {
                if (type.BaseType != null)
                {
                    string parentName = type.BaseType.FullName.LeftOf('<').Replace('`', JS.Vars.D);

                    if (!allTypes.ContainsKey(parentName))
                    {
                        Bridge.Translator.TranslatorException.Throw("Unknown type {0}", parentName);
                    }

                    if (!result.Contains(parentName))
                    {
                        result.Add(parentName);
                    }
                }
            }
            return result;
        }

        public virtual void CheckFileName(TypeDefinition type, ITranslator translator)
        {
            var fileName = type.GetFileName();
            if (fileName != null)
            {
                var typeInfo = this.EnsureTypeInfo(type, translator);
                typeInfo.FileName = fileName;
            }
        }

        public virtual void CheckModule(TypeDefinition type, ITranslator translator)
        {
            var typeInfo = this.EnsureTypeInfo(type, translator);
            var module = type.GetModule();
            if (module != null)
            {
                typeInfo.Module = module;
            }
        }


        public virtual void CheckModuleDependenies(TypeDefinition type, ITranslator translator)
        {
            var attr = type.GetModuleDependency();

            if (attr != null)
            {
                var typeInfo = this.EnsureTypeInfo(type, translator);
                typeInfo.Dependencies.Add(attr);
            }
        }

        protected virtual ITypeInfo EnsureTypeInfo(TypeDefinition type, ITranslator translator)
        {
            string key = BridgeTypes.GetTypeDefinitionKey(type);
            ITypeInfo typeInfo = null;

            if (translator.TypeInfoDefinitions.ContainsKey(key))
            {
                typeInfo = translator.TypeInfoDefinitions[key];
            }
            else
            {
                typeInfo = new TypeInfo();
                translator.TypeInfoDefinitions[key] = typeInfo;
            }
            return typeInfo;
        }

        public virtual bool IsDelegateOrLambda(ResolveResult result)
        {
            return result.Type.Kind == ICSharpCode.NRefactory.TypeSystem.TypeKind.Delegate || result is LambdaResolveResult;
        }

        public virtual void CheckIdentifier(string name, AstNode context)
        {
            if (Helpers.IsReservedWord(null, name))
            {
                throw new EmitterException(context, "Cannot use '" + name + "' as identifier");
            }
        }
    }
}