
namespace System.ComponentModel.DataAnnotations.Schema
{
    /// <summary>
    /// Specifies the inverse of a navigation property that represents the other end of the same relationship.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    [Bridge.External]
    [Bridge.NonScriptable]
    public class InversePropertyAttribute : Attribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="InversePropertyAttribute" /> class.
        /// </summary>
        /// <param name="property">The navigation property representing the other end of the same relationship.</param>
        public extern InversePropertyAttribute(string property);

        /// <summary>
        /// The navigation property representing the other end of the same relationship.
        /// </summary>
        public extern string Property { get; }
    }
}
