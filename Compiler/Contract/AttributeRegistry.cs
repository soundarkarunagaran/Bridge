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
        public static IEnumerable<ICSharpCode.NRefactory.CSharp.Attribute> GetBridgeAttributes(this EntityDeclaration entity)
        {
            return entity.Attributes.SelectMany(s => s.Attributes);
        }
        public static IEnumerable<IAttribute> GetBridgeAttributes(this IEntity entity)
        {
            return entity.Attributes;
        }
        public static IEnumerable<IAttribute> GetBridgeAttributes(this IMember member)
        {
            return member.Attributes;
        }
        public static IEnumerable<IAttribute> GetBridgeAttributes(this IParameter member)
        {
            return member.Attributes;
        }
        public static IEnumerable<CustomAttribute> GetBridgeAttributes(this ICustomAttributeProvider member)
        {
            return member.CustomAttributes;
        }
        public static IEnumerable<IAttribute> GetBridgeAttributes(this IAssembly assembly)
        {
            return assembly.AssemblyAttributes;
        }
    }
}
