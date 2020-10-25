using System;
using JetBrains.Annotations;

namespace Auxilia
{
    /// <summary>
    /// Contains extensions for any <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> when the given <see cref="object"/> is <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="obj">Object to check.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>The given object.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown when the value is <see langword="null"/>.</exception>
        [ContractAnnotation("obj:null => false")]
        public static T ThrowIfNull<T>([NoEnumeration] this T obj, string valueName)
        {
            return obj ?? throw new ArgumentNullException(valueName, "Cannot be null.");
        }
    }
}