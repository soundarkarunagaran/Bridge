using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Bridge.Contract.Constants;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;

namespace Bridge.Contract
{
    /// <summary>
    /// This registry centralizes the access to attributes to unify the logic and allow
    /// injection of attributes during compiletime. 
    /// </summary>
    public static class AttributeRegistry
    {

        public static bool IsCompilerGenerated(this ICustomAttributeProvider owner)
        {
            return owner.HasAttribute(CS.Attributes.COMPILER_GENERATED_NAME);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>null if the entity does not have a GlobalMethodsAttribute, otherwise the bool indicates the scoped flag</returns>
        public static bool? HasGlobalMethodsAttribute(this IEntity entity)
        {
            var globalMethods = entity.GetAttribute("Bridge.GlobalMethodsAttribute");
            if (globalMethods == null)
            {
                return null;
            }

            return globalMethods.PositionalArguments.Count > 0 && (bool)globalMethods.PositionalArguments.First().ConstantValue;
        }

        public static string GetMixin(this IEntity entity)
        {
            var mixin = entity.GetAttribute("Bridge.MixinAttribute");

            if (mixin == null)
            {
                return null;
            }
            var value = mixin.PositionalArguments.First().ConstantValue as string;
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidDataException(string.Format("The argument to the [MixinAttribute] for the type {0} must not be null or empty.", entity.FullName));
            }

            return value;
        }

        public static bool IsNonScriptable(this IEntity entity)
        {
            return entity.HasAttribute("Bridge.NonScriptableAttribute");
        }

        public static Module GetModule(this ITypeDefinition typeDefinition)
        {
            var attr = typeDefinition.GetAttribute("Bridge.ModuleAttribute");
            if (attr != null)
            {
                return ReadModuleFromAttribute(attr);
            }

            var asm = typeDefinition.ParentAssembly;
            attr = asm.GetAttribute("Bridge.ModuleAttribute");
            if (attr != null)
            {
                return ReadModuleFromAttribute(attr);
            }

            return null;
        }

        public static Module GetModule(this TypeDefinition typeDefinition)
        {
            var attr = typeDefinition.GetAttribute("Bridge.ModuleAttribute");
            if (attr != null)
            {
                return ReadModuleFromAttribute(attr);
            }

            var asm = typeDefinition.Module.Assembly;
            attr = asm.GetAttribute("Bridge.ModuleAttribute");
            if (attr != null)
            {
                return ReadModuleFromAttribute(attr);
            }

            return null;
        }

        public static ModuleDependency GetModuleDependency(this TypeDefinition typeDefinition)
        {
            var attr = typeDefinition.GetAttribute("Bridge.ModuleDependencyAttribute");
            if (attr == null || !attr.HasConstructorArguments)
            {
                return null;
            }

            ModuleDependency dependency = new ModuleDependency();
            var obj = attr.ConstructorArguments[0].Value;
            dependency.DependencyName = obj is string ? obj.ToString() : "";

            if (attr.ConstructorArguments.Count > 1)
            {
                obj = attr.ConstructorArguments[1].Value;
                dependency.VariableName = obj is string ? obj.ToString() : "";
            }

            return dependency;
        }

        public static bool IsAccessorsIndexer(this IEntity entity)
        {
            if (entity == null)
            {
                return false;
            }

            if (entity.HasAttribute(CS.Attributes.ACCESSORSINDEXER_ATTRIBUTE_NAME))
            {
                return true;
            }

            return false;
        }


        private static Module ReadModuleFromAttribute(IAttribute attr)
        {
            Module module = null;

            if (attr.PositionalArguments.Count == 1)
            {
                var obj = attr.PositionalArguments[0].ConstantValue;

                if (obj is bool)
                {
                    module = new Module((bool)obj);
                }
                else if (obj is string)
                {
                    module = new Module(obj.ToString());
                }
                else if (obj is int)
                {
                    module = new Module("", (ModuleType)(int)obj);
                }
                else
                {
                    module = new Module();
                }
            }
            else if (attr.PositionalArguments.Count == 2)
            {
                if (attr.PositionalArguments[0].ConstantValue is string)
                {
                    var name = attr.PositionalArguments[0].ConstantValue;
                    var preventName = attr.PositionalArguments[1].ConstantValue;

                    module = new Module(name != null ? name.ToString() : "", (bool)preventName);
                }
                else if (attr.PositionalArguments[1].ConstantValue is bool)
                {
                    var mtype = attr.PositionalArguments[0].ConstantValue;
                    var preventName = attr.PositionalArguments[1].ConstantValue;

                    module = new Module("", (ModuleType)(int)mtype, (bool)preventName);
                }
                else
                {
                    var mtype = attr.PositionalArguments[0].ConstantValue;
                    var name = attr.PositionalArguments[1].ConstantValue;

                    module = new Module(name != null ? name.ToString() : "", (ModuleType)(int)mtype);
                }
            }
            else if (attr.PositionalArguments.Count == 3)
            {
                var mtype = attr.PositionalArguments[0].ConstantValue;
                var name = attr.PositionalArguments[1].ConstantValue;
                var preventName = attr.PositionalArguments[2].ConstantValue;

                module = new Module(name != null ? name.ToString() : "", (ModuleType)(int)mtype, (bool)preventName);
            }
            else
            {
                module = new Module();
            }

            if (attr.NamedArguments.Count > 0)
            {
                foreach (var namedArgument in attr.NamedArguments)
                {
                    if (namedArgument.Key.Name == "Name")
                    {
                        module.Name = namedArgument.Value.ConstantValue != null ? (string)namedArgument.Value.ConstantValue : "";
                    }
                    else if (namedArgument.Key.Name == "ExportAsNamespace")
                    {
                        module.ExportAsNamespace = namedArgument.Value.ConstantValue != null ? (string)namedArgument.Value.ConstantValue : "";
                    }
                }
            }

            return module;
        }

        private static Module ReadModuleFromAttribute(CustomAttribute attr)
        {
            Module module = null;

            if (attr.ConstructorArguments.Count == 1)
            {
                var obj = attr.ConstructorArguments[0].Value;

                if (obj is bool)
                {
                    module = new Module((bool)obj);
                }
                else if (obj is string)
                {
                    module = new Module(obj.ToString());
                }
                else if (obj is int)
                {
                    module = new Module("", (ModuleType)(int)obj);
                }
                else
                {
                    module = new Module();
                }
            }
            else if (attr.ConstructorArguments.Count == 2)
            {
                if (attr.ConstructorArguments[0].Value is string)
                {
                    var name = attr.ConstructorArguments[0].Value;
                    var preventName = attr.ConstructorArguments[1].Value;

                    module = new Module(name != null ? name.ToString() : "", (bool)preventName);
                }
                else if (attr.ConstructorArguments[1].Value is bool)
                {
                    var mtype = attr.ConstructorArguments[0].Value;
                    var preventName = attr.ConstructorArguments[1].Value;

                    module = new Module("", (ModuleType)(int)mtype, (bool)preventName);
                }
                else
                {
                    var mtype = attr.ConstructorArguments[0].Value;
                    var name = attr.ConstructorArguments[1].Value;

                    module = new Module(name != null ? name.ToString() : "", (ModuleType)(int)mtype);
                }
            }
            else if (attr.ConstructorArguments.Count == 3)
            {
                var mtype = attr.ConstructorArguments[0].Value;
                var name = attr.ConstructorArguments[1].Value;
                var preventName = attr.ConstructorArguments[2].Value;

                module = new Module(name != null ? name.ToString() : "", (ModuleType)(int)mtype, (bool)preventName);
            }
            else
            {
                module = new Module();
            }

            if (attr.Properties.Count > 0)
            {
                foreach (var namedArgument in attr.Properties)
                {
                    if (namedArgument.Name == "Name")
                    {
                        module.Name = namedArgument.Argument.Value != null ? (string)namedArgument.Argument.Value : "";
                    }
                    else if (namedArgument.Name == "ExportAsNamespace")
                    {
                        module.ExportAsNamespace = namedArgument.Argument.Value != null ? (string)namedArgument.Argument.Value : "";
                    }
                }
            }

            return module;
        }

        public static bool IsIgnoreGeneric(this ITypeDefinition type)
        {
            return type.HasAttribute("Bridge.IgnoreGenericAttribute") || type.DeclaringTypeDefinition != null && IsIgnoreGeneric(type.DeclaringTypeDefinition);
        }

        public static bool IsIgnoreGeneric(this IEntity type)
        {
            return type.HasAttribute("Bridge.IgnoreGenericAttribute") || type.DeclaringTypeDefinition != null && IsIgnoreGeneric(type.DeclaringTypeDefinition);
        }

        public static bool IsIgnoreGeneric(this TypeDefinition type)
        {
            return type.HasAttribute("Bridge.IgnoreGenericAttribute") || type.DeclaringType != null && IsIgnoreGeneric(type.DeclaringType);
        }

        public static bool IsIgnoreGeneric(this IType type, bool allowInTypeScript = false)
        {
            var attr = type.GetDefinition().GetAttribute("Bridge.IgnoreGenericAttribute");

            if (attr != null)
            {
                var member = allowInTypeScript ? attr.NamedArguments.FirstOrDefault(arg => arg.Key.Name == "AllowInTypeScript").Value : null;

                if (member != null)
                {
                    return !(bool)member.ConstantValue;
                }

                return true;
            }

            return type.DeclaringType != null && IsIgnoreGeneric(type.DeclaringType, allowInTypeScript);
        }

        public static bool IsIgnoreCast(this ITypeDefinition typeDef)
        {
            if (AssemblyInfo != null && AssemblyInfo.IgnoreCast)
            {
                return true;
            }

            if (typeDef == null)
            {
                return false;
            }

            if (typeDef.Kind == TypeKind.Delegate)
            {
                return true;
            }

            var attr = typeDef.GetAttribute("Bridge.ConstructorAttribute");
            if (attr != null)
            {
                var inline = attr.PositionalArguments[0].ConstantValue.ToString();
                if (Regex.Match(inline, @"\s*\{\s*\}\s*").Success)
                {
                    return true;
                }
            }

            return typeDef.HasAttribute("Bridge.IgnoreCastAttribute") || typeDef.HasAttribute("Bridge.ObjectLiteralAttribute");
        }

        public static bool IsScript(this MethodDefinition method)
        {
            return method.HasAttribute(CS.NS.BRIDGE + ".ScriptAttribute");
        }

        public static bool IsScript(this IMethod method)
        {
            return method.HasAttribute(CS.NS.BRIDGE + ".ScriptAttribute");
        }

        public static bool HasNameAttribute(this IEntity method)
        {
            return GetNameAttribute(method) != null;
        }

        public static IAttribute GetNameAttribute(this IEntity method)
        {
            return method.GetAttribute(CS.NS.BRIDGE + ".NameAttribute", true);
        }

        public static IAttribute GetNameAttribute(this IParameter method)
        {
            return method.GetAttribute(CS.NS.BRIDGE + ".NameAttribute");
        }

        public static IAttribute GetConventionAttribute(this IEntity method)
        {
            return method.GetAttribute(CS.NS.BRIDGE + ".ConventionAttribute");
        }
        public static IEnumerable<IAttribute> GetConventionAttributes(this IEntity entity)
        {
            return entity.GetAttributes(CS.NS.BRIDGE + ".ConventionAttribute");
        }
        public static IEnumerable<IAttribute> GetConventionAttributes(this IAssembly assembly)
        {
            return assembly.GetAttributes(CS.NS.BRIDGE + ".ConventionAttribute");
        }

        public static IAttribute GetRule(this IEntity entity)
        {
            return entity.GetAttribute(CS.NS.BRIDGE + ".RulesAttribute", true);
        }
        public static IEnumerable<IAttribute> GetRules(this IEntity entity)
        {
            return entity.GetAttributes(CS.NS.BRIDGE + ".RulesAttribute", true);
        }
        public static IEnumerable<IAttribute> GetRules(this IAssembly assembly)
        {
            return assembly.GetAttributes(CS.NS.BRIDGE + ".RulesAttribute");
        }

        public static IAttribute GetReflectableAttribute(this IEntity entity)
        {
            var attr = entity.GetAttribute("Bridge.ReflectableAttribute");
            if (attr != null)
            {
                return attr;
            }

            // fallback to potentially inherited attribute
            attr = entity.GetAttribute("Bridge.ReflectableAttribute", true);
            if (attr != null && (bool)attr.NamedArguments.First(arg => arg.Key.Name == "Inherits").Value.ConstantValue)
            {
                return attr;
            }

            return attr;
        }

        public static bool HasReadyAttribute(this IEntity entity)
        {
            return entity.HasAttribute(CS.Attributes.READY_ATTRIBUTE_NAME);
        }


        public static bool ExpandParams(this IEntity entity)
        {
            return entity.HasAttribute("Bridge.ExpandParamsAttribute");
        }

        public static bool IsExternal(this IEntity entity)
        {
            return entity.HasAttribute("Bridge.ExternalAttribute")
                || entity.HasAttribute("Bridge.IgnoreAttribute")
                || entity.HasAttribute("Bridge.NonScriptableAttribute")
                || IsVirtual(entity as ITypeDefinition)
                || (entity.DeclaringTypeDefinition != null && IsExternal(entity.DeclaringTypeDefinition))
                || (entity.DeclaringTypeDefinition != null && IsExternal(entity.DeclaringTypeDefinition.ParentAssembly))
            ;
        }

        public static bool IsExternal(this TypeDefinition entity)
        {
            return entity.HasAttribute("Bridge.ExternalAttribute")
                || entity.HasAttribute("Bridge.IgnoreAttribute")
                || entity.HasAttribute("Bridge.NonScriptableAttribute")
                || IsVirtual(entity)
                || (entity.DeclaringType != null && IsExternal(entity.DeclaringType))
                || (entity.DeclaringType != null && IsExternal(entity.Module.Assembly))
            ;
        }
        public static bool IsExternal(this AssemblyDefinition entity)
        {
            return entity.HasAttribute("Bridge.ExternalAttribute")
                || entity.HasAttribute("Bridge.IgnoreAttribute")
                || entity.HasAttribute("Bridge.NonScriptableAttribute")
            ;
        }

        public static bool IsVirtual(this ITypeDefinition typeDef)
        {
            if (typeDef == null)
            {
                return false;
            }
            return typeDef != null && IsVirtualType(typeDef);
        }

        public static bool IsVirtual(this TypeDefinition typeDef)
        {
            if (typeDef == null)
            {
                return false;
            }
            return typeDef != null && IsVirtualType(typeDef);
        }

        public static bool IsVirtualType(ITypeDefinition typeDefinition)
        {
            var attr = typeDefinition.GetAttribute("Bridge.VirtualAttribute");
            if (attr == null && typeDefinition.DeclaringType != null)
            {
                return IsVirtualType(typeDefinition.DeclaringType.GetDefinition());
            }

            if (attr == null)
            {
                attr = typeDefinition.ParentAssembly.GetAttribute("Bridge.VirtualAttribute");
            }

            bool isVirtual = false;

            if (attr != null)
            {
                if (attr.PositionalArguments.Count == 0)
                {
                    isVirtual = true;
                }
                else
                {
                    var value = (int)attr.PositionalArguments[0].ConstantValue;

                    switch (value)
                    {
                        case 0:
                            isVirtual = true;
                            break;
                        case 1:
                            isVirtual = typeDefinition.Kind != TypeKind.Interface;
                            break;
                        case 2:
                            isVirtual = typeDefinition.Kind == TypeKind.Interface;
                            break;
                    }
                }
            }

            return isVirtual;
        }


        public static bool IsVirtualType(TypeDefinition typeDefinition)
        {
            var attr = typeDefinition.GetAttribute("Bridge.VirtualAttribute");
            if (attr == null && typeDefinition.DeclaringType != null)
            {
                return IsVirtualType(typeDefinition.DeclaringType);
            }

            if (attr == null)
            {
                attr = typeDefinition.Module.Assembly.GetAttribute("Bridge.VirtualAttribute");
            }

            bool isVirtual = false;

            if (attr != null)
            {
                if (attr.ConstructorArguments.Count == 0)
                {
                    isVirtual = true;
                }
                else
                {
                    var value = (int)attr.ConstructorArguments[0].Value;

                    switch (value)
                    {
                        case 0:
                            isVirtual = true;
                            break;
                        case 1:
                            isVirtual = !typeDefinition.IsInterface;
                            break;
                        case 2:
                            isVirtual = typeDefinition.IsInterface;
                            break;
                    }
                }
            }

            return isVirtual;
        }

        public static bool IsExternal(this IAssembly entity)
        {
            return entity.HasAttribute("Bridge.ExternalAttribute");
        }

        public static bool HasFlagsAttribute(this ITypeDefinition entity)
        {
            return entity.HasAttribute("System.FlagsAttribute");
        }

        public static IAttribute GetInitAttribute(this IEntity entity)
        {
            return entity.GetAttribute("Bridge.InitAttribute");
        }

        public static AttributeUsageAttribute GetAttributeUsage(this IEntity entity)
        {
            var aua = entity.GetBridgeAttributes().FirstOrDefault(a => a.AttributeType.FullName == "System.AttributeUsageAttribute");
            if (aua == null || aua.PositionalArguments.Count == 0)
            {
                return null;
            }

            var validOn = (AttributeTargets)(int)aua.PositionalArguments[0].ConstantValue;
            var attr = new AttributeUsageAttribute(validOn);
            foreach (var arg in aua.NamedArguments)
            {
                switch (arg.Key.Name)
                {
                    case "AllowMultiple":
                        attr.AllowMultiple = (bool) arg.Value.ConstantValue;
                        break;
                    case "Inherited":
                        attr.Inherited = (bool) arg.Value.ConstantValue;
                        break;
                }
            }
            return attr;
        }

        public static IEnumerable<string> GetScript(this IEntity entity)
        {
            var attr = entity.GetAttribute("Bridge.ScriptAttribute");
            if (attr != null && attr.PositionalArguments.Count > 0)
            {
                return attr.PositionalArguments[0].ConstantValue as IEnumerable<string>;
            }
            return null;
        }

        public static IEnumerable<IAttribute> GetAdapterAttributes(this IEntity entity)
        {
            var allAttrs = entity.GetBridgeAttributes();
            var adapters = new List<IAttribute>();
            foreach (var attr in allAttrs)
            {
                var baseTypes = attr.AttributeType.GetAllBaseTypes().ToArray();
                if (baseTypes.Any(t => t.FullName == "Bridge.AdapterAttribute"))
                {
                    adapters.Add(attr);
                }
            }
            return adapters;
        }



        public static int EnumEmitMode(this ITypeDefinition type)
        {
            int result = 7;

            var attr = type.GetAttribute("Bridge.EnumAttribute");
            if (attr != null && attr.PositionalArguments.Count > 0)
            {
                result = (int)attr.PositionalArguments.First().ConstantValue;
            }

            return result;
        }

        public static int? GetObjectLiteralMode(this ITypeDefinition type)
        {
            var attr = type.GetAttribute("Bridge.ObjectLiteralAttribute");
            if (attr == null || attr.PositionalArguments.Count < 1)
            {
                return null;
            }

            var value = attr.PositionalArguments[0].ConstantValue;
            if (value is int)
            {
                return (int)value;
            }

            return null;
        }

        public static string GetCastCode(this ITypeDefinition type)
        {
            var attr = type.GetAttribute("Bridge.CastAttribute");
            if (attr == null || attr.PositionalArguments.Count < 1)
            {
                return null;
            }
            return attr.PositionalArguments[0].ConstantValue as string;
        }

        public static string GetTemplate(this IEntity type, IEmitter emitter)
        {
            bool delegated;
            return GetTemplate(type, emitter, out delegated);
        }

        public static bool? UnboxingAllowed(this IEntity type)
        {
            var attr = type.GetAttribute("Bridge.UnboxAttribute", true);
            if (attr == null || attr.PositionalArguments.Count < 1)
            {
                return null;
            }

            var value = attr.PositionalArguments[0].ConstantValue;
            if (value is bool)
            {
                return (bool)value;
            }

            return null;
        }

        public static bool HasTemplate(this IEntity type)
        {
            return type.HasAttribute("Bridge.TemplateAttribute");
        }


        public static bool HasScript(this IEntity type)
        {
            return type.HasAttribute("Bridge.ScriptAttribute");
        }


        public static string GetTemplate(this IEntity type, IEmitter emitter, out bool delegated, bool convertDelegateToTemplate = true)
        {
            delegated = false;

            var attr = type.GetAttribute("Bridge.TemplateAttribute");
            if (attr == null)
            {
                return null;
            }

            string inlineCode = null;
            var namedArg = attr.NamedArguments.FirstOrDefault(arg => arg.Key.Name == CS.Attributes.Template.PROPERTY_FN);
            if (namedArg.Key != null)
            {
                delegated = true;
                inlineCode = namedArg.Value.ConstantValue.ToString();
                if (type is IMethod && convertDelegateToTemplate)
                {
                    inlineCode = Helpers.DelegateToTemplate(inlineCode, (IMethod)type, emitter);
                }
            }

            if (inlineCode == null)
            {
                inlineCode = attr != null && attr.PositionalArguments.Count > 0 ? attr.PositionalArguments[0].ConstantValue.ToString() : null;
            }

            if (!string.IsNullOrEmpty(inlineCode) && (type is IProperty || type is IEvent))
            {
                inlineCode = inlineCode.Replace("{value}", "{0}");
            }

            return inlineCode;
        }

        public static bool IsInlineMethod(this IEntity type)
        {
            var attr = type.GetAttribute("Bridge.TemplateAttribute");
            if (attr == null)
            {
                return false;
            }
            return attr.PositionalArguments.Count == 0 && attr.NamedArguments.Count == 0;
        }

        public static IEnumerable<IAttribute> GetScriptableAttributes(this IAssembly assembly, SyntaxTree tree = null)
        {
            var attributes = assembly.GetBridgeAttributes();
            return GetScriptableAttributes(attributes, tree);
        }

        public static IEnumerable<IAttribute> GetScriptableAttributes(this IEntity entity, SyntaxTree tree = null)
        {
            var attributes = entity.GetBridgeAttributes();
            return GetScriptableAttributes(attributes, tree);
        }

        public static IEnumerable<IAttribute> GetScriptableAttributes(this IParameter entity, SyntaxTree tree = null)
        {
            var attributes = entity.GetBridgeAttributes();
            return GetScriptableAttributes(attributes, tree);
        }

        public static IEnumerable<IAttribute> GetScriptableAttributes(IEnumerable<IAttribute> attributes, SyntaxTree tree = null)
        {
            return attributes.Where(a =>
            {
                var typeDef = a.AttributeType.GetDefinition();
                return typeDef != null && !IsConditionallyRemoved(a, tree) && typeDef.IsExternal() &&
                       !typeDef.IsNonScriptable();
            });
        }

        public static bool IsConditionallyRemoved(this IEntity entity, IEnumerable<string> conditionalSymbols)
        {
            if (entity == null || conditionalSymbols == null)
            {
                return false;
            }

            var result = entity.GetAttributes("System.Diagnostics.ConditionalAttribute")
                .Where(a => a.PositionalArguments.Count > 0)
                .Select(a => a.PositionalArguments[0].ConstantValue as string)
                .Where(s => s != null)
                .ToList();

            if (result.Count > 0)
            {
                return !result.Intersect(conditionalSymbols).Any();
            }

            return false;
        }


        private static bool IsConditionallyRemoved(IAttribute attr, SyntaxTree tree)
        {
            var typeDef = attr.AttributeType.GetDefinition();
            if (typeDef != null)
            {
                var symbols = FindConditionalSymbols(typeDef);

                if (symbols.Count > 0)
                {
                    if (tree != null)
                    {
                        return !symbols.Intersect(tree.ConditionalSymbols).Any();
                    }
                    else
                    {
                        return !symbols.Intersect(AssemblyInfo.DefineConstants).Any();
                    }
                }

                return false;
            }
            return false;
        }

        private static IList<string> FindConditionalSymbols(IEntity entity)
        {
            var result = new List<string>();
            foreach (var a in entity.GetBridgeAttributes())
            {
                var type = a.AttributeType.GetDefinition();
                if (type != null && type.FullName.Equals("System.Diagnostics.ConditionalAttribute", StringComparison.Ordinal))
                {
                    if (a.PositionalArguments.Count > 0)
                    {
                        var symbol = a.PositionalArguments[0].ConstantValue as string;
                        if (symbol != null)
                        {
                            result.Add(symbol);
                        }
                    }
                }
            }
            return result;
        }

        public static bool IsMemberConst(this IMember member)
        {
            var field = member as IField;
            if (field != null)
            {
                return field.IsConst && member.DeclaringType.Kind != TypeKind.Enum;
            }

            return false;
        }

        public static bool IsInlineConst(this IMember member)
        {
            bool isConst = IsMemberConst(member);

            if (isConst)
            {
                var attr = member.GetBridgeAttributes().FirstOrDefault(a => a.AttributeType.FullName == "Bridge.InlineConstAttribute");
                if (attr != null)
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetAssemblyDescription(this ICustomAttributeProvider provider)
        {
            string assemblyDescription = null;

            var assemblyDescriptionAttribute = provider.GetBridgeAttributes().FirstOrDefault(x => x.AttributeType.FullName == "System.Reflection.AssemblyDescriptionAttribute");

            if (assemblyDescriptionAttribute != null
                && assemblyDescriptionAttribute.HasConstructorArguments)
            {
                assemblyDescription = assemblyDescriptionAttribute.ConstructorArguments[0].Value as string;
            }

            if (assemblyDescription != null)
            {
                assemblyDescription = assemblyDescription.Trim();
            }

            return assemblyDescription;

        }

        public static string GetAssemblyTitle(this ICustomAttributeProvider provider)
        {
            string assemblyDescription = null;

            var assemblyDescriptionAttribute = provider.GetBridgeAttributes().FirstOrDefault(x => x.AttributeType.FullName == "System.Reflection.AssemblyTitleAttribute");

            if (assemblyDescriptionAttribute != null
                && assemblyDescriptionAttribute.HasConstructorArguments)
            {
                assemblyDescription = assemblyDescriptionAttribute.ConstructorArguments[0].Value as string;
            }

            if (assemblyDescription != null)
            {
                assemblyDescription = assemblyDescription.Trim();
            }

            return assemblyDescription;
        }

        /// <summary>
        /// Makes any existing nested types (classes?) inherit the FileName attribute of the specified type.
        /// Does not override a nested type's FileName attribute if present.
        /// </summary>
        /// <param name="type"></param>
        public static void InheritAttributesToNestedTypes(this TypeDefinition type)
        {
            // List of attribute names that are meant to be inherited by sub-classes.
            var attrList = new List<string>
            {
                "FileNameAttribute",
                "ModuleAttribute",
                "NamespaceAttribute"
            };

            foreach (var attributeName in attrList)
            {
                var attr = type.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == attributeName);
                if (attr != null)
                {
                    foreach (var nestedType in type.NestedTypes)
                    {
                        if (nestedType.CustomAttributes.All(ca => ca.AttributeType.Name != attributeName))
                        {
                            nestedType.CustomAttributes.Add(attr);
                        }
                    }
                }
            }
        }


        public static Tuple<bool, string> IsGlobalTarget(this IMember member)
        {
            var attr = member.GetBridgeAttributes(true).FirstOrDefault(a => a.AttributeType.FullName == "Bridge.GlobalTargetAttribute" && a.PositionalArguments.Count > 0);
            if (attr == null)
            {
                return null;
            }
            return Tuple.Create(true, (string)attr.PositionalArguments.First().ConstantValue);
        }

        public static int? GetPriority(this ICustomAttributeProvider priority)
        {
            var attr = priority.GetBridgeAttributes().FirstOrDefault(a => a.AttributeType.FullName == "Bridge.GlobalTargetAttribute" && a.ConstructorArguments.Count > 0);
            if (attr == null)
            {
                return null;
            }
            return Convert.ToInt32(attr.ConstructorArguments[0].Value);
        }

        public static bool IsExternalInterface(this ITypeDefinition typeDefinition, out bool isNative)
        {
            string externalAttr = "Bridge.ExternalInterfaceAttribute";
            var attr = typeDefinition.GetBridgeAttributes().FirstOrDefault(a => a.Constructor != null && (a.Constructor.DeclaringType.FullName == externalAttr));

            if (attr == null)
            {
                attr = typeDefinition.ParentAssembly.GetBridgeAttributes().FirstOrDefault(a => a.Constructor != null && (a.Constructor.DeclaringType.FullName == externalAttr));
            }

            isNative = attr != null && attr.PositionalArguments.Count == 1 && (bool)attr.PositionalArguments[0].ConstantValue;

            if (attr == null)
            {
                isNative = typeDefinition.ParentAssembly.AssemblyName == CS.NS.BRIDGE || !IsExternal(typeDefinition);
            }

            return attr != null;
        }

        public static IExternalInterface IsExternalInterface(this ITypeDefinition typeDefinition)
        {
            string externalAttr = "Bridge.ExternalInterfaceAttribute";
            var attr = typeDefinition.GetBridgeAttributes().FirstOrDefault(a => a.Constructor != null && (a.Constructor.DeclaringType.FullName == externalAttr));

            if (attr == null)
            {
                attr = typeDefinition.ParentAssembly.GetBridgeAttributes().FirstOrDefault(a => a.Constructor != null && (a.Constructor.DeclaringType.FullName == externalAttr));
            }

            if (attr != null)
            {
                var ei = new ExternalInterface();
                if (attr.PositionalArguments.Count == 1)
                {
                    bool isNative = (bool)attr.PositionalArguments[0].ConstantValue;

                    if (isNative)
                    {
                        ei.IsNativeImplementation = true;
                    }
                    else
                    {
                        ei.IsSimpleImplementation = true;
                    }
                }

                if (attr.NamedArguments.Count == 1)
                {
                    if (attr.NamedArguments[0].Key.Name == "IsVirtual")
                    {
                        ei.IsVirtual = (bool)attr.NamedArguments[0].Value.ConstantValue;
                    }
                }

                return ei;
            }

            return null;
        }

        public static bool IsImmutableType(this ICustomAttributeProvider type)
        {
            return type.HasAttribute("Bridge.ImmutableAttribute");
        }

        public static bool IsImmutableType(this IEntity type)
        {
            return type.HasAttribute("Bridge.ImmutableAttribute");
        }

        public static int GetObjectCreateMode(this IEntity type)
        {
            var attr = type.GetAttribute("Bridge.ObjectLiteralAttribute");
            if (attr != null)
            {
                foreach (var t in attr.PositionalArguments)
                {
                    if (t.Type.FullName == "Bridge.ObjectCreateMode")
                    {
                        return (int)t.ConstantValue;
                    }
                }
            }
            return 0;
        }

        public static bool HasFieldAttribute(this IMember member, bool checkAssembly = true)
        {
            var fieldAttributeName = "Bridge.FieldAttribute";
            var autoPropertyToField = member.HasAttribute(fieldAttributeName);

            if (!autoPropertyToField)
            {
                autoPropertyToField = member.DeclaringTypeDefinition.HasAttribute(fieldAttributeName);
            }

            if (!autoPropertyToField && checkAssembly)
            {
                autoPropertyToField = member.DeclaringTypeDefinition.ParentAssembly.HasAttribute(fieldAttributeName);
            }

            return autoPropertyToField;
        }



        public static Tuple<string, bool> GetNamespace(this ICustomAttributeProvider provider)
        {
            var attr = provider.GetAttribute("Bridge.NamespaceAttribute");
            if (attr == null || !attr.HasConstructorArguments)
            {
                return null;
            }

            if (attr.ConstructorArguments[0].Value is string)
            {
                return Tuple.Create((string)attr.ConstructorArguments[0].Value, false);
            }

            if (attr.ConstructorArguments[0].Value is bool)
            {
                return Tuple.Create((string)null, (bool)attr.ConstructorArguments[0].Value);
            }

            return null;
        }

        public static Tuple<string, bool> GetNamespace(this IEntity provider)
        {
            var attr = provider.GetAttribute("Bridge.NamespaceAttribute");
            if (attr == null || attr.PositionalArguments.Count == 0)
            {
                return null;
            }

            if (attr.PositionalArguments[0].ConstantValue is string)
            {
                return Tuple.Create((string)attr.PositionalArguments[0].ConstantValue, false);
            }

            if (attr.PositionalArguments[0].ConstantValue is bool)
            {
                return Tuple.Create((string)null, (bool)attr.PositionalArguments[0].ConstantValue);
            }

            return null;
        }


        public static string GetFileName(this ICustomAttributeProvider provider)
        {
            var attr = provider.GetAttribute("Bridge.FileNameAttribute");
            if (attr == null || !attr.HasConstructorArguments)
            {
                return null;
            }

            if (attr.ConstructorArguments[0].Value is string)
            {
                return (string)attr.ConstructorArguments[0].Value;
            }

            return null;
        }


        public static Tuple<string, bool> GetName(this ICustomAttributeProvider provider)
        {
            var attr = provider.GetAttribute("Bridge.NameAttribute");
            if (attr == null || !attr.HasConstructorArguments)
            {
                return null;
            }

            if (attr.ConstructorArguments[0].Value is string)
            {
                return Tuple.Create((string)attr.ConstructorArguments[0].Value, false);
            }

            if (attr.ConstructorArguments[0].Value is bool)
            {
                return Tuple.Create((string)null, (bool)attr.ConstructorArguments[0].Value);
            }

            return null;
        }

        public static string GetCustomConstructor(this IEntity provider)
        {
            var attr = provider.GetAttribute("Bridge.ConstructorAttribute");
            if (attr != null && attr.PositionalArguments.Count > 0)
            {
                return (string)attr.PositionalArguments[0].ConstantValue;
            }

            if (provider.IsObjectLiteral() && provider.GetObjectCreateMode() == 0)
            {
                return "{ }";
            }

            return null;
        }


        public static string GetCustomConstructor(this TypeDefinition provider)
        {
            var attr = provider.GetAttribute("Bridge.ConstructorAttribute");
            if (attr != null && attr.ConstructorArguments.Count > 0)
            {
                return (string)attr.ConstructorArguments[0].Value;
            }

            if (provider.IsObjectLiteral() && provider.GetObjectCreateMode() == 0)
            {
                return "{ }";
            }

            return null;
        }

        public static int GetObjectCreateMode(this ICustomAttributeProvider type)
        {
            var attr = type.GetAttribute("Bridge.ObjectLiteralAttribute");
            if (attr != null && attr.HasConstructorArguments)
            {
                foreach (var t in attr.ConstructorArguments)
                {
                    if (t.Type.FullName == "Bridge.ObjectCreateMode")
                    {
                        return (int)t.Value;
                    }
                }
            }
            return 0;
        }

        public static int GetObjectInitializationMode(this ICustomAttributeProvider type)
        {
            var attr = type.GetAttribute("Bridge.ObjectLiteralAttribute");
            if (attr != null && attr.HasConstructorArguments)
            {
                foreach (CustomAttributeArgument t in attr.ConstructorArguments)
                {
                    if (t.Type.FullName == "Bridge.ObjectInitializationMode")
                    {
                        return (int)t.Value;
                    }
                }
            }
            return 0;
        }

        public static bool IsObjectLiteral(this ICustomAttributeProvider type)
        {
            return type.HasAttribute("Bridge.ObjectLiteralAttribute");
        }

        public static bool IsObjectLiteral(this IEntity type)
        {
            return type.HasAttribute("Bridge.ObjectLiteralAttribute");
        }



        private static bool HasAttribute(this ICustomAttributeProvider entity, string attributeName)
        {
            return GetAttribute(entity, attributeName) != null;
        }

        private static CustomAttribute GetAttribute(this ICustomAttributeProvider entity, string attributeName)
        {
            if (entity == null) return null;
            return entity.GetBridgeAttributes().FirstOrDefault(b => attributeName == b.AttributeType.FullName);
        }

        private static bool HasAttribute(this IAssembly entity, string attributeName)
        {
            return GetAttribute(entity, attributeName) != null;
        }

        private static IAttribute GetAttribute(this IAssembly entity, string attributeName)
        {
            if (entity == null) return null;
            return entity.GetBridgeAttributes().FirstOrDefault(b => attributeName == b.AttributeType.FullName);
        }

        private static IEnumerable<IAttribute> GetAttributes(this IAssembly entity, string attributeName)
        {
            if (entity == null) return Enumerable.Empty<IAttribute>();
            return entity.GetBridgeAttributes().Where(b => attributeName == b.AttributeType.FullName);
        }

        private static IAttribute GetAttribute(this IParameter entity, string attributeName)
        {
            if (entity == null) return null;
            return entity.GetBridgeAttributes().FirstOrDefault(b => attributeName == b.AttributeType.FullName);
        }

        private static bool HasAttribute(this IEntity entity, string attributeName, bool includeInherited = false)
        {
            return GetAttribute(entity, attributeName, includeInherited) != null;
        }

        private static IAttribute GetAttribute(this IEntity entity, string attributeName, bool includeInherited = false)
        {
            if (entity == null) return null;
            return entity.GetBridgeAttributes(includeInherited).FirstOrDefault(b => attributeName == b.AttributeType.FullName);
        }

        private static IEnumerable<IAttribute> GetAttributes(this IEntity entity, string attributeName, bool includeInherited = false)
        {
            if (entity == null) return Enumerable.Empty<IAttribute>();
            return entity.GetBridgeAttributes(includeInherited).Where(b => attributeName == b.AttributeType.FullName);
        }

        public static IEnumerable<ICSharpCode.NRefactory.CSharp.Attribute> GetBridgeAttributes(this EntityDeclaration entity)
        {
            if (entity == null) return Enumerable.Empty<ICSharpCode.NRefactory.CSharp.Attribute>();
            return entity.Attributes.SelectMany(s => s.Attributes);
        }

        public static IEnumerable<IAttribute> GetBridgeAttributes(this IEntity entity, bool includeInherited = false)
        {
            if (entity == null) return Enumerable.Empty<IAttribute>();

            var attributes = new List<IAttribute>(entity.Attributes);
            if (includeInherited)
            {
                attributes.AddRange(GetInheritedAttributes(entity));
            }
            return attributes;
        }

        public static IEnumerable<IAttribute> GetBridgeReturnTypeAttributes(this IMethod entity, bool includeInherited = false)
        {
            if (entity == null) return Enumerable.Empty<IAttribute>();

            var attributes = new List<IAttribute>(entity.ReturnTypeAttributes);
            if (includeInherited)
            {
                attributes.AddRange(GetInheritedAttributes(entity));
            }
            return attributes;
        }
        public static IEnumerable<IAttribute> GetBridgeAttributes(this IParameter member)
        {
            if (member == null) return Enumerable.Empty<IAttribute>();
            return member.Attributes ?? Enumerable.Empty<IAttribute>();
        }
        public static IEnumerable<CustomAttribute> GetBridgeAttributes(this ICustomAttributeProvider member)
        {
            if (member == null) return Enumerable.Empty<CustomAttribute>();
            return member.CustomAttributes ?? Enumerable.Empty<CustomAttribute>();
        }
        public static IEnumerable<IAttribute> GetBridgeAttributes(this IAssembly assembly)
        {
            if (assembly == null) return Enumerable.Empty<IAttribute>();
            return assembly.AssemblyAttributes ?? Enumerable.Empty<IAttribute>();
        }

        private static IEnumerable<IAttribute> GetInheritedAttributes(IEntity entity)
        {
            if (entity is IMember)
            {
                return GetInheritedAttributes((IMember)entity);
            }
            return Enumerable.Empty<IAttribute>();
        }

        private static IEnumerable<IAttribute> GetInheritedAttributes(IMember member)
        {
            if (member.IsOverride)
            {
                member = InheritanceHelper.GetBaseMember(member);

                if (member != null)
                {
                    return member.GetBridgeAttributes(true);
                }
            }
            else if (member.ImplementedInterfaceMembers != null && member.ImplementedInterfaceMembers.Count > 0)
            {
                return member.ImplementedInterfaceMembers.SelectMany(
                    m => m.GetBridgeAttributes(true)
                );
            }

            return Enumerable.Empty<IAttribute>();
        }

        public static IAssemblyInfo AssemblyInfo { get; set; }
    }
}
