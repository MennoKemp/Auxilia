using System;
using System.ComponentModel;
using System.Linq;

namespace Auxilia.Extensions
{
    /// <summary>
    /// Contains extensions for objects of type <see cref="Enum"/>.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if the given <see langword="enum"/> value is defined.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <returns>True if the given value is defined; false, otherwise.</returns>
        public static bool IsDefined<TEnum>(this TEnum value) where TEnum : Enum
        {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
            return Enum.IsDefined(typeof(TEnum), value);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
        }

        /// <summary>
        /// Gets a description from the <see cref="DescriptionAttribute"/> of an <see cref="Enum"/> value if specified.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="value">Value to get the description of.</param>
        /// <returns>Value description when the <see cref="DescriptionAttribute"/> can be found; the value name, otherwise.</returns>
        public static string GetDescription<TEnum>(this TEnum value) where TEnum : Enum
        {
            string valueName = value.ToString();

            try
            {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
                return (typeof(TEnum)
                           .GetMember(valueName)
                           .FirstOrDefault()?
                           .GetCustomAttributes(typeof(DescriptionAttribute), false)
                           .FirstOrDefault()
                       as DescriptionAttribute)?
                   .Description
                   ?? valueName;
#pragma warning restore ExceptionNotDocumented // Exception is not documented
            }
            catch
            {
                return valueName;
            }
        }

        /// <summary>
        /// Throws an <see cref="EnumValueNotDefinedException{TEnum}"/> when the given value has not been defined.
        /// </summary>
        /// <typeparam name="TEnum">Enum type.</typeparam>
        /// <param name="value">Value to check.</param>
        /// <param name="valueName">Name of the value (optional).</param>
        /// <returns>Enum value.</returns>
        /// <exception cref="EnumValueNotDefinedException{TEnum}">Thrown when the given value has not been defined.</exception>
        public static TEnum ThrowIfNotDefined<TEnum>(this TEnum value, string valueName = null) where TEnum : Enum
        {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
            return Enum.IsDefined(typeof(TEnum), value)
                ? value
                : throw new EnumValueNotDefinedException<TEnum>(value, valueName);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
        }
    }
}
