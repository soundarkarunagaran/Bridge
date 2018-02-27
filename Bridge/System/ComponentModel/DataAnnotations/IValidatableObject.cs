using System.Collections.Generic;

namespace System.ComponentModel.DataAnnotations
{
    [Bridge.External]
    [Bridge.NonScriptable]
    public interface IValidatableObject
    {
        IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
