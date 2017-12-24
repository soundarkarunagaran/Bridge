using System;
using System.Linq.Expressions;

namespace Bridge.CompilerServices
{
    [ExternalInterface]
    [NonScriptable]
    public interface IAttributesBuilder
    {
        IAttributesBuilder Add(Attribute attribute);
    }
}