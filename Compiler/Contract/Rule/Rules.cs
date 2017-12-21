using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge.Contract
{
    public class Rules
    {

        public static CompilerRule Default = new CompilerRule
        {
            Lambda = LambdaRule.Managed,
            AnonymousType = AnonymousTypeRule.Managed,
            Integer = IntegerRule.Managed,
            Boxing = BoxingRule.Managed,
            ArrayIndex = ArrayIndexRule.Managed,
            AutoProperty = null
        };

        public static CompilerRule Get(IEmitter emitter, IEntity entity)
        {
            CompilerRule memberRule = null;
            CompilerRule[] classRules = null;
            CompilerRule[] assemblyRules = null;
            CompilerRule[] interfaceRules = null;

            if (entity is IMember)
            {
                memberRule = entity.GetRule(CompilerRuleLevel.Member);
                var typeDef = entity.DeclaringTypeDefinition;

                if (typeDef != null)
                {
                    classRules = Rules.GetClassRules(emitter, typeDef);
                }

                interfaceRules = Rules.GetVirtualMemberRules(emitter, entity);
            }
            else if (entity is ITypeDefinition)
            {
                classRules = Rules.GetClassRules(emitter, (ITypeDefinition)entity);
            }

            var assembly = entity.ParentAssembly;

            if (emitter != null && emitter.AssemblyCompilerRuleCache.ContainsKey(assembly))
            {
                assemblyRules = emitter.AssemblyCompilerRuleCache[assembly];
            }
            else
            {
                assemblyRules = assembly.GetRules(CompilerRuleLevel.Assembly)
                    .ToArray();

                if(emitter != null)
                {
                    emitter.AssemblyCompilerRuleCache.Add(assembly, assemblyRules);
                }                
            }

            var rules = new List<CompilerRule>();

            if (memberRule != null)
            {
                rules.Add(memberRule);
            }

            if (classRules != null && classRules.Length > 0)
            {
                rules.AddRange(classRules);
            }

            if (interfaceRules != null && interfaceRules.Length > 0)
            {
                rules.AddRange(interfaceRules);
            }

            if(emitter != null)
            {
                rules.Add(emitter.AssemblyInfo.Rules);
            }            

            if (assemblyRules != null && assemblyRules.Length > 0)
            {
                rules.AddRange(assemblyRules);
            }

            return MergeRules(rules);
        }

        private static CompilerRule MergeRules(List<CompilerRule> rules)
        {
            var resultRule = new CompilerRule
            {
                Lambda = LambdaRule.Managed,
                AnonymousType = AnonymousTypeRule.Managed,
                Integer = IntegerRule.Managed,
                Boxing = BoxingRule.Managed,
                ArrayIndex = ArrayIndexRule.Managed,
                AutoProperty = null
            };

            for (int i = rules.Count - 1; i >= 0; i--)
            {
                var rule = rules[i];

                if (rule.Lambda.HasValue)
                {
                    resultRule.Lambda = rule.Lambda;
                }

                if (rule.AutoProperty.HasValue)
                {
                    resultRule.AutoProperty = rule.AutoProperty;
                }

                if (rule.AnonymousType.HasValue)
                {
                    resultRule.AnonymousType = rule.AnonymousType;
                }

                if (rule.Integer.HasValue)
                {
                    resultRule.Integer = rule.Integer;
                }

                if (rule.ArrayIndex.HasValue)
                {
                    resultRule.ArrayIndex = rule.ArrayIndex;
                }

                if (rule.Boxing.HasValue)
                {
                    resultRule.Boxing = rule.Boxing;
                }
            }

            return resultRule;
        }


        private static CompilerRule[] GetVirtualMemberRules(IEmitter emitter, IEntity entity)
        {
            var member = entity as IMember;

            if (member != null)
            {
                if (member.IsOverride)
                {
                    var baseMember = InheritanceHelper.GetBaseMember(member);
                    return new[] { Rules.Get(emitter, baseMember) };
                }

                if (member.ImplementedInterfaceMembers.Count > 0)
                {
                    var interfaceMember = member.ImplementedInterfaceMembers.First();
                    return Rules.GetClassRules(emitter, interfaceMember.DeclaringTypeDefinition);
                }
            }

            return null;
        }

        private static CompilerRule[] GetClassRules(IEmitter emitter, ITypeDefinition typeDef)
        {
            if (emitter != null && emitter.ClassCompilerRuleCache.ContainsKey(typeDef))
            {
                return emitter.ClassCompilerRuleCache[typeDef];
            }

            var td = typeDef;
            List<CompilerRule> rules = new List<CompilerRule>();
            while (td != null)
            {
                rules.AddRange(td.GetRules(CompilerRuleLevel.Class));
                td = td.DeclaringTypeDefinition;
            }

            var classRules = rules.ToArray();
            if (emitter != null)
            {
                emitter.ClassCompilerRuleCache.Add(typeDef, classRules);
            }
            
            return classRules;
        }
    }

    public enum CompilerRuleLevel
    {
        None,
        Assembly,
        Class,
        Member
    }
}
