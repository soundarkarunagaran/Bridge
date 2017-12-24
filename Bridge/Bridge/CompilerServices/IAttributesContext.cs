using System;
using System.Linq.Expressions;

namespace Bridge.CompilerServices
{
    [ExternalInterface]
    [NonScriptable]
    public interface IAttributesContext
    {
        IAttributesBuilder Assembly();
        IAttributesBuilder Assembly(string name);
        IAttributesBuilder Type<TType>();
        IAttributesBuilder Constructor<TType>(Expression<Func<TType>> ctor);
        IAttributesBuilder Member<TType>(Expression<Action<TType>> member, AttributeTarget target = AttributeTarget.Default, string targetName = null);
        IAttributesBuilder Member<TType,TReturn>(Expression<Func<TType,TReturn>> member, AttributeTarget target = AttributeTarget.Default, string targetName = null);
        IAttributesBuilder Event<TType>(string member, AttributeTarget target = AttributeTarget.Default);
    }
}