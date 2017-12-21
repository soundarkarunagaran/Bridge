using Bridge.Contract;
using Bridge.Contract.Constants;

using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using System.Collections.Generic;
using System.Linq;
using NRAttribute = ICSharpCode.NRefactory.CSharp.Attribute;

namespace Bridge.Translator
{
    public partial class Inspector : Visitor
    {
        internal IEmitter Emitter { get; private set; }

        public Inspector(IAssemblyInfo config = null)
        {
            this.Types = new List<ITypeInfo>();
            this.IgnoredTypes = new List<string>();
            this.AssemblyInfo = config ?? new AssemblyInfo();

            this.Emitter = new TempEmitter { AssemblyInfo = this.AssemblyInfo };
        }

        private Expression GetDefaultFieldInitializer(AstType type)
        {
            return new PrimitiveExpression(Inspector.GetDefaultFieldValue(type, this.Resolver), "?");
        }

        public static object GetDefaultFieldValue(AstType type, IMemberResolver resolver)
        {
            if (type is PrimitiveType)
            {
                var primitiveType = (PrimitiveType)type;

                switch (primitiveType.KnownTypeCode)
                {
                    case KnownTypeCode.Decimal:
                        return 0m;

                    case KnownTypeCode.Int64:
                        return 0L;

                    case KnownTypeCode.UInt64:
                        return 0UL;

                    case KnownTypeCode.Int16:
                    case KnownTypeCode.Int32:
                    case KnownTypeCode.UInt16:
                    case KnownTypeCode.UInt32:
                    case KnownTypeCode.Byte:
                    case KnownTypeCode.Double:
                    case KnownTypeCode.SByte:
                    case KnownTypeCode.Single:
                        return 0;

                    case KnownTypeCode.Boolean:
                        return false;
                }
            }

            var resolveResult = resolver.ResolveNode(type, null);

            var o = GetDefaultFieldValue(resolveResult.Type, type, false);

            if (o != null)
            {
                return o;
            }

            if (!resolveResult.IsError && NullableType.IsNullable(resolveResult.Type))
            {
                return null;
            }

            if (!resolveResult.IsError && (resolveResult.Type.IsKnownType(KnownTypeCode.Enum) || resolveResult.Type.Kind == TypeKind.Enum))
            {
                return 0;
            }

            if (!resolveResult.IsError && resolveResult.Type.Kind == TypeKind.Struct)
            {
                return type;
            }

            return null;
        }

        public static object GetDefaultFieldValue(IType type, AstType astType, bool wrapType = true)
        {
            if (type.Kind == TypeKind.TypeParameter && astType != null)
            {
                var parameter = type as ITypeParameter;
                if (parameter != null && (
                    parameter.Owner.IsIgnoreGeneric()||
                    parameter.Owner.DeclaringTypeDefinition != null && parameter.Owner.DeclaringTypeDefinition.IsIgnoreGeneric()))
                {
                    return null;
                }
                return new RawValue(JS.Funcs.BRIDGE_GETDEFAULTVALUE + "(" + type.Name + ")");
            }

            if (type.IsKnownType(KnownTypeCode.Decimal))
            {
                return 0m;
            }

            if (type.IsKnownType(KnownTypeCode.Int64))
            {
                return 0L;
            }

            if (type.IsKnownType(KnownTypeCode.UInt64))
            {
                return 0UL;
            }

            if (type.IsKnownType(KnownTypeCode.Char) ||
                type.IsKnownType(KnownTypeCode.Int16) ||
                type.IsKnownType(KnownTypeCode.Int32) ||
                type.IsKnownType(KnownTypeCode.UInt16) ||
                type.IsKnownType(KnownTypeCode.UInt32) ||
                type.IsKnownType(KnownTypeCode.Byte) ||
                type.IsKnownType(KnownTypeCode.Double) ||
                type.IsKnownType(KnownTypeCode.SByte) ||
                type.IsKnownType(KnownTypeCode.Single) ||
                type.IsKnownType(KnownTypeCode.Enum))
            {
                return 0;
            }

            if (NullableType.IsNullable(type))
            {
                return null;
            }

            if (type.IsKnownType(KnownTypeCode.Boolean))
            {
                return false;
            }

            if (type.IsKnownType(KnownTypeCode.Enum) || type.Kind == TypeKind.Enum)
            {
                return 0;
            }

            if (type.Kind == TypeKind.Struct && wrapType)
            {
                return type;
            }

            return null;
        }

        public static string GetStructDefaultValue(AstType type, IEmitter emitter)
        {
            var rr = emitter.Resolver.ResolveNode(type, emitter);
            return GetStructDefaultValue(rr.Type, emitter);
        }

        public static string GetStructDefaultValue(IType type, IEmitter emitter)
        {
            if (type.IsKnownType(KnownTypeCode.DateTime))
            {
                return string.Format("{0}()", JS.Types.System.DateTime.GET_DEFAULT_VALUE);
            }

            var isGeneric = type.TypeArguments.Count > 0 && !type.IsIgnoreGeneric();

            return string.Concat("new ", isGeneric ? "(" : "", BridgeTypes.ToJsName(type, emitter), isGeneric ? ")" : "", "()");
        }

        protected virtual bool IsValidStaticInitializer(Expression expr)
        {
            if (expr.IsNull || expr is PrimitiveExpression)
            {
                return true;
            }

            var arrayExpr = expr as ArrayCreateExpression;

            if (arrayExpr == null)
            {
                return false;
            }

            try
            {
                new ArrayInitializerVisitor().VisitArrayCreateExpression(arrayExpr);

                return true;
            }
            catch (TranslatorException)
            {
                return false;
            }
        }

        protected virtual void FixMethodParameters(AstNodeCollection<ParameterDeclaration> parameters, BlockStatement body)
        {
            /*if (parameters.Count == 0)
            {
                return;
            }

            foreach (var p in parameters)
            {
                string newName = JS.Vars.FIX_ARGUMENT_NAME + p.Name;
                string oldName = p.Name;

                VariableDeclarationStatement varState = new VariableDeclarationStatement(p.Type.Clone(), oldName, new CastExpression(p.Type.Clone(), new IdentifierExpression(newName)));

                p.Name = newName;

                body.InsertChildBefore(body.FirstChild, varState, new Role<VariableDeclarationStatement>("Statement"));
            }*/
        }

        /// <summary>
        /// Checks if the namespace name is likely to conflict with Bridge.NET namespace.
        /// </summary>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        protected static bool IsConflictingNamespace(string namespaceName)
        {
            return (namespaceName == Translator.Bridge_ASSEMBLY);
        }

        /// <summary>
        /// Validates the type's namespace attribute (if present) against conflicts with Bridge.NET's namespaces.
        /// </summary>
        /// <param name="tpDecl">The TypeDefinition object of the validated item.</param>
        private void ValidateNamespace(TypeDeclaration tpDecl)
        {
            if (this.AssemblyInfo.Assembly.EnableReservedNamespaces)
            {
                return;
            }

            var rr = Resolver.ResolveNode(tpDecl, this.Emitter);
            if (rr.Type != null)
            {
                var nsName = rr.Type.GetDefinition().GetNamespace();
                if (nsName != null && Bridge.Translator.Inspector.IsConflictingNamespace(nsName.Item1))
                {
                    throw new EmitterException(tpDecl.NameToken, "Custom attribute '[" + rr.Type.FullName +
                                                     "]' uses reserved namespace name 'Bridge'."
                                                     + Bridge.Translator.Emitter.NEW_LINE
                                                     + "This name is reserved for Bridge.NET core.");
                }
            }
        }

        /// <summary>
        /// Validates the namespace name against conflicts with Bridge.NET's namespaces.
        /// </summary>
        /// <param name="nsDecl">The NamespaceDefinition object of the validated item.</param>
        private void ValidateNamespace(NamespaceDeclaration nsDecl)
        {
            if (Bridge.Translator.Inspector.IsConflictingNamespace(nsDecl.FullName))
            {
                throw new EmitterException(nsDecl, "Namespace '" + nsDecl.FullName +
                    "' uses reserved name 'Bridge'."
                    + Bridge.Translator.Emitter.NEW_LINE
                    + "This name is reserved for Bridge.NET core.");
            }
        }
    }
}