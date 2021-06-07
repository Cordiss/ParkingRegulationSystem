using System;

namespace Diploma.Helpers
{
    /// <summary>
    /// Defines DependsOfProperty attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DependsOnPropertyAttribute : Attribute
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="propertyName">Parent property name.</param>
        public DependsOnPropertyAttribute(string propertyName)
        {
            Property = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        }

        /// <summary>
        /// Gets parent property name.
        /// </summary>
        public string Property { get; }
    }
}