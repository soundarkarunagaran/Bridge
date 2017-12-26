using Bridge.Contract;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Threading.Tasks;
using ICSharpCode.NRefactory.TypeSystem;

namespace Bridge.Translator
{
    public partial class Translator
    {
        public Stack<string> CurrentAssemblyLocationInspected
        {
            get; set;
        } = new Stack<string>();

        private class CecilAssemblyResolver : DefaultAssemblyResolver
        {
            public ILogger Logger
            {
                get; set;
            }

            public CecilAssemblyResolver(ILogger logger, string location)
            {
                this.Logger = logger;

                this.ResolveFailure += CecilAssemblyResolver_ResolveFailure;

                this.AddSearchDirectory(Path.GetDirectoryName(location));
            }

            private AssemblyDefinition CecilAssemblyResolver_ResolveFailure(object sender, AssemblyNameReference reference)
            {
                string fullName = reference != null ? reference.FullName : "";
                this.Logger.Trace("CecilAssemblyResolver: ResolveFailure " + (fullName ?? ""));

                return null;
            }

            public override AssemblyDefinition Resolve(string fullName)
            {
                this.Logger.Trace("CecilAssemblyResolver: Resolve(string) " + (fullName ?? ""));

                return base.Resolve(fullName);
            }

            public override AssemblyDefinition Resolve(AssemblyNameReference name)
            {
                string fullName = name != null ? name.FullName : "";

                this.Logger.Trace("CecilAssemblyResolver: Resolve(AssemblyNameReference) " + (fullName ?? ""));

                return base.Resolve(name);
            }

            public override AssemblyDefinition Resolve(string fullName, ReaderParameters parameters)
            {
                this.Logger.Trace(
                    "CecilAssemblyResolver: Resolve(string, ReaderParameters) "
                    + (fullName ?? "")
                    + ", "
                    + (parameters != null ? parameters.ReadingMode.ToString() : "")
                    );

                return base.Resolve(fullName, parameters);
            }

            public override AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
            {
                string fullName = name != null ? name.FullName : "";

                this.Logger.Trace(
                    "CecilAssemblyResolver: Resolve(AssemblyNameReference, ReaderParameters) "
                    + (fullName ?? "")
                    + ", "
                    + (parameters != null ? parameters.ReadingMode.ToString() : "")
                    );

                return base.Resolve(name, parameters);
            }
        }

        protected virtual AssemblyDefinition LoadAssembly(string location, List<AssemblyDefinition> references)
        {
            this.Log.Trace("Assembly definition loading " + (location ?? "") + " ...");

            if (CurrentAssemblyLocationInspected.Contains(location))
            {
                var message = string.Format("There is a circular reference found for assembly location {0}. To avoid the error, rename your project's assembly to be different from that location.", location);

                this.Log.Error(message);
                throw new System.InvalidOperationException(message);
            }

            CurrentAssemblyLocationInspected.Push(location);

            var assemblyDefinition = AssemblyDefinition.ReadAssembly(
                    location,
                    new ReaderParameters()
                    {
                        ReadingMode = ReadingMode.Deferred,
                        AssemblyResolver = new CecilAssemblyResolver(this.Log, this.AssemblyLocation)
                    }
                );

            foreach (AssemblyNameReference r in assemblyDefinition.MainModule.AssemblyReferences)
            {
                var name = r.Name;

                if (name == SystemAssemblyName || name == "System.Core")
                {
                    continue;
                }

                var fullName = r.FullName;

                if (references.Any(a => a.Name.FullName == fullName))
                {
                    continue;
                }

                var path = Path.Combine(Path.GetDirectoryName(location), name) + ".dll";

                var updateBridgeLocation = name.ToLowerInvariant() == "bridge" && (string.IsNullOrWhiteSpace(this.BridgeLocation) || !File.Exists(this.BridgeLocation));

                if (updateBridgeLocation)
                {
                    this.BridgeLocation = path;
                }

                var reference = this.LoadAssembly(path, references);

                if (reference != null && !references.Any(a => a.Name.FullName == reference.Name.FullName))
                {
                    references.Add(reference);
                }
            }

            this.Log.Trace("Assembly definition loading " + (location ?? "") + " done");

            var cl = CurrentAssemblyLocationInspected.Pop();

            if (cl != location)
            {
                throw new System.InvalidOperationException(string.Format("Current location {0} is not the current location in stack {1}", location, cl));
            }

            return assemblyDefinition;
        }

        public virtual void ReadTypes(ICompilation compilation)
        {
            if (compilation.MainAssembly.AssemblyName != Translator.Bridge_ASSEMBLY || this.AssemblyInfo.Assembly != null && this.AssemblyInfo.Assembly.EnableReservedNamespaces)
            {
                this.ReadTypes(compilation.MainAssembly);
            }

            foreach (var item in compilation.ReferencedAssemblies)
            {
                this.ReadTypes(item);
            }

        }
        protected virtual void ReadTypes(IAssembly assembly)
        {
            this.Log.Trace("Reading types for assembly " + assembly.FullAssemblyName + " ...");

            this.AddNestedTypes(assembly.TopLevelTypeDefinitions);

            this.Log.Trace("Reading types for assembly done");
        }

        protected virtual void AddNestedTypes(IEnumerable<ITypeDefinition> types)
        {
            foreach (var type in types.OrderBy(t => t.FullName))
            {
                if (type.FullName.Contains("<"))
                {
                    continue;
                }

                string key = BridgeTypes.GetTypeDefinitionKey(type);

                BridgeType duplicateBridgeType = null;

                if (this.BridgeTypes.TryGetValue(key, out duplicateBridgeType))
                {
                    var duplicate = duplicateBridgeType.Type.GetDefinition();

                    var message = string.Format(
                        Constants.Messages.Exceptions.DUPLICATE_BRIDGE_TYPE,
                        type.ParentAssembly.FullAssemblyName,
                        type.FullName,
                        duplicate.ParentAssembly.FullAssemblyName,
                        duplicate.FullName);

                    this.Log.Error(message);
                    throw new System.InvalidOperationException(message);
                }

                var typeInfo = this.EnsureTypeInfo(type);
                this.BridgeTypes.Add(key, new BridgeType(key)
                {
                    Type = type,
                    TypeInfo = typeInfo
                });

                if (!type.IsExternal() && type.Kind != TypeKind.Delegate)
                {
                    var fileName = type.GetFileName();
                    if (fileName != null)
                    {
                        typeInfo.FileName = fileName;
                    }
                    var module = type.GetModule();
                    if (module != null)
                    {
                        typeInfo.Module = module;
                    }
                    var dependency = type.GetModuleDependency();
                    if (dependency != null)
                    {
                        typeInfo.Dependencies.Add(dependency);
                    }
                }

                if (type.NestedTypes != null)
                {
                    this.AddNestedTypes(type.NestedTypes);
                }
            }
        }

        protected virtual ITypeInfo EnsureTypeInfo(ITypeDefinition type)
        {
            string key = BridgeTypes.GetTypeDefinitionKey(type);
            ITypeInfo typeInfo = null;

            if (TypeInfoDefinitions.ContainsKey(key))
            {
                typeInfo = TypeInfoDefinitions[key];
            }
            else
            {
                typeInfo = new TypeInfo();
                TypeInfoDefinitions[key] = typeInfo;
            }
            return typeInfo;
        }

        protected virtual List<AssemblyDefinition> InspectReferences()
        {
            this.Log.Info("Inspecting references...");

            this.TypeInfoDefinitions = new Dictionary<string, ITypeInfo>();

            var references = new List<AssemblyDefinition>();
            var assembly = this.LoadAssembly(this.AssemblyLocation, references);
            this.BridgeTypes = new BridgeTypes();
            this.AssemblyDefinition = assembly;

            if (!this.FolderMode)
            {
                var prefix = Path.GetDirectoryName(this.Location);

                for (int i = 0; i < this.SourceFiles.Count; i++)
                {
                    this.SourceFiles[i] = Path.Combine(prefix, this.SourceFiles[i]);
                }
            }

            this.Log.Info("Inspecting references done");

            return references;
        }

        protected virtual void InspectTypes(MemberResolver resolver, IAssemblyInfo config)
        {
            this.Log.Info("Inspecting types...");

            Inspector inspector = this.CreateInspector(config);
            inspector.AssemblyInfo = config;
            inspector.Resolver = resolver;

            for (int i = 0; i < this.ParsedSourceFiles.Count; i++)
            {
                var sourceFile = this.ParsedSourceFiles[i];
                this.Log.Trace("Visiting syntax tree " + (sourceFile != null && sourceFile.ParsedFile != null && sourceFile.ParsedFile.FileName != null ? sourceFile.ParsedFile.FileName : ""));

                inspector.VisitSyntaxTree(sourceFile.SyntaxTree);
            }

            this.AssemblyInfo = inspector.AssemblyInfo;
            this.Types = inspector.Types;

            this.ReadTypes(resolver.Compilation);

            this.Log.Info("Inspecting types done");
        }

        protected virtual Inspector CreateInspector(IAssemblyInfo config = null)
        {
            return new Inspector(config);
        }

        private string[] Rewrite()
        {
            this.Log.Info("Rewriting new C# features...");
            var rewriter = new SharpSixRewriter(this);
            var result = new string[this.SourceFiles.Count];
            Task.WaitAll(this.SourceFiles.Select((file, index) => Task.Run(() => result[index] = new SharpSixRewriter(rewriter).Rewrite(index))).ToArray());
            this.Log.Info("Rewriting new C# features done...");

            return result;
        }

        protected void BuildSyntaxTree()
        {
            this.Log.Info("Building syntax tree...");

            var rewriten = Rewrite();

            for (int i = 0; i < this.SourceFiles.Count; i++)
            {
                var fileName = this.SourceFiles[i];

                this.Log.Trace("Source file " + (fileName ?? string.Empty) + " ...");

                var parser = new ICSharpCode.NRefactory.CSharp.CSharpParser();

                if (AssemblyInfo.DefineConstants != null && AssemblyInfo.DefineConstants.Count > 0)
                {
                    foreach (var defineConstant in AssemblyInfo.DefineConstants)
                    {
                        parser.CompilerSettings.ConditionalSymbols.Add(defineConstant);
                    }
                }

                var syntaxTree = parser.Parse(rewriten[i], fileName);
                syntaxTree.FileName = fileName;
                //var syntaxTree = parser.Parse(reader, fileName);
                this.Log.Trace("\tParsing syntax tree done");

                if (parser.HasErrors)
                {
                    foreach (var error in parser.Errors)
                    {
                        throw new EmitterException(syntaxTree, string.Format("Parsing error in a file {0} {2}: {1}", fileName, error.Message, error.Region.Begin.ToString()));
                    }
                }

                var expandResult = new QueryExpressionExpander().ExpandQueryExpressions(syntaxTree);
                this.Log.Trace("\tExpanding query expressions done");

                syntaxTree = (expandResult != null ? (SyntaxTree)expandResult.AstNode : syntaxTree);

                var emptyLambdaDetecter = new EmptyLambdaDetecter();
                syntaxTree.AcceptVisitor(emptyLambdaDetecter);
                this.Log.Trace("\tAccepting lambda detector visitor done");

                if (emptyLambdaDetecter.Found)
                {
                    var fixer = new EmptyLambdaFixer();
                    var astNode = syntaxTree.AcceptVisitor(fixer);
                    this.Log.Trace("\tAccepting lambda fixer visitor done");
                    syntaxTree = (astNode != null ? (SyntaxTree)astNode : syntaxTree);
                    syntaxTree.FileName = fileName;
                }

                var f = new ParsedSourceFile(syntaxTree, new CSharpUnresolvedFile
                {
                    FileName = fileName
                });
                this.ParsedSourceFiles.Add(f);

                var tcv = new TypeSystemConvertVisitor(f.ParsedFile);
                f.SyntaxTree.AcceptVisitor(tcv);
                this.Log.Trace("\tAccepting type system convert visitor done");

                this.Log.Trace("Source file " + (fileName ?? string.Empty) + " done");
            }

            this.Log.Info("Building syntax tree done");
        }
    }
}