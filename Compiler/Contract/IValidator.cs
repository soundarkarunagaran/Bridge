using ICSharpCode.NRefactory.TypeSystem;
using Mono.Cecil;

namespace Bridge.Contract
{
    public interface IValidator
    {
        bool CanIgnoreType(Mono.Cecil.TypeDefinition type);

        void CheckIdentifier(string name, ICSharpCode.NRefactory.CSharp.AstNode context);

        void CheckConstructors(Mono.Cecil.TypeDefinition type, ITranslator translator);

        void CheckFields(Mono.Cecil.TypeDefinition type, ITranslator translator);

        void CheckFileName(Mono.Cecil.TypeDefinition type, ITranslator translator);

        void CheckMethodArguments(Mono.Cecil.MethodDefinition method);

        void CheckMethods(Mono.Cecil.TypeDefinition type, ITranslator translator);

        void CheckModule(Mono.Cecil.TypeDefinition type, ITranslator translator);

        void CheckModuleDependenies(Mono.Cecil.TypeDefinition type, ITranslator translator);

        void CheckProperties(Mono.Cecil.TypeDefinition type, ITranslator translator);

        void CheckType(Mono.Cecil.TypeDefinition type, ITranslator translator);

        string GetCustomTypeName(Mono.Cecil.TypeDefinition type, IEmitter emitter, bool excludeNs);

        System.Collections.Generic.HashSet<string> GetParentTypes(System.Collections.Generic.IDictionary<string, Mono.Cecil.TypeDefinition> allTypes);

        bool IsDelegateOrLambda(ICSharpCode.NRefactory.Semantics.ResolveResult result);

        bool IsBridgeClass(Mono.Cecil.TypeDefinition type);
    }
}