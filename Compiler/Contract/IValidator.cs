using ICSharpCode.NRefactory.TypeSystem;

namespace Bridge.Contract
{
    public interface IValidator
    {
        bool CanIgnoreType(ITypeDefinition type);

        void CheckIdentifier(string name, ICSharpCode.NRefactory.CSharp.AstNode context);

        void CheckConstructors(ITypeDefinition type, ITranslator translator);

        void CheckFields(ITypeDefinition type, ITranslator translator);

        void CheckFileName(ITypeDefinition type, ITranslator translator);

        void CheckMethodArguments(IMethod method);

        void CheckMethods(ITypeDefinition type, ITranslator translator);

        void CheckModule(ITypeDefinition type, ITranslator translator);

        void CheckModuleDependenies(ITypeDefinition type, ITranslator translator);

        void CheckProperties(ITypeDefinition type, ITranslator translator);

        void CheckType(ITypeDefinition type, ITranslator translator);

        string GetCustomTypeName(ITypeDefinition type, IEmitter emitter, bool excludeNs);

        System.Collections.Generic.HashSet<string> GetParentTypes(System.Collections.Generic.IDictionary<string, ITypeDefinition> allTypes);

        bool IsDelegateOrLambda(ICSharpCode.NRefactory.Semantics.ResolveResult result);

        bool IsBridgeClass(ITypeDefinition type);
    }
}