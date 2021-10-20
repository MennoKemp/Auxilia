using System;

namespace Auxilia
{
    /// <summary>
    /// Exception thrown when an enum value is not defined.
    /// </summary>
    /// <typeparam name="T">Enum type.</typeparam>
    public class EnumValueNotDefinedException<T> : Exception where T : Enum
    {
        /// <summary>
        /// Initializes new instance of <see cref="EnumValueNotDefinedException{T}"/>.
        /// </summary>
        /// <param name="value">Value which is not defined.</param>
        public EnumValueNotDefinedException(T value)
            : base($"Value '{value}' is not defined.")
        {
            Value = value;
        }

        /// <summary>
        /// Initializes new instance of <see cref="EnumValueNotDefinedException{T}"/>.
        /// </summary>
        /// <param name="value">Value which is not defined.</param>
        /// <param name="valueName">Name of the value.</param>
        public EnumValueNotDefinedException(T value, string valueName)
            : base($"Value '{value}' is not defined for '{typeof(T).Name}'.")
        {
            Value = value;
            ValueName = valueName;
        }

        /// <summary>
        /// Gets the undefined value.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Gets the name of the enum value.
        /// </summary>
        public string ValueName { get; }
    }
}