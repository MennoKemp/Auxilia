using System;
using System.Collections.Generic;

namespace Auxilia.Extensions
{
    /// <summary>
    /// Contains extensions for objects of type <see cref="List{T}"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Swaps element at index <paramref name="index1"/> with the element at index <paramref name="index2"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Enumeration containing the values to swap.</param>
        /// <param name="index1">First index of the element to swap.</param>
        /// <param name="index2">Second index of the element to swap.</param>
        /// <returns>List with swapped values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index1"/> or <paramref name="index2"/> is outside the boundaries of the values.</exception>
        /// <exception cref="OverflowException">Thrown when the number of elements in source is larger than <see cref="Int32.MaxValue"/>.</exception>
        public static IList<T> Swap<T>(this IList<T> source, int index1, int index2)
        {
            source.ThrowIfNull(nameof(source));

            if (!source.IsWithinBounds(index1))
                throw new ArgumentOutOfRangeException(nameof(index1), index1, "Index is outside the boundaries.");

            if (!source.IsWithinBounds(index2))
                throw new ArgumentOutOfRangeException(nameof(index2), index2, "Index is outside the boundaries.");

#pragma warning disable ExceptionNotDocumented // Exception is not documented
            T value1 = source[index1];
            T value2 = source[index2];

            source[index1] = value2;
            source[index2] = value1;
#pragma warning restore ExceptionNotDocumented // Exception is not documented

            return source;
        }
    }
}
