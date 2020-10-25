using System;

namespace Auxilia
{
    /// <summary>
    /// Used to add a description to an element.
    /// </summary>
    public class DescriptionAttribute : Attribute
    {
        /// <summary>
        /// Initializes new instance of <see cref="DescriptionAttribute"/>.
        /// </summary>
        /// <param name="description">Element description.</param>
        public DescriptionAttribute(string description)
        {
            Description = description ?? string.Empty;
        }

        /// <summary>
        /// Element description.
        /// </summary>
        public string Description { get; }
    }
}