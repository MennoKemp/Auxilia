using System;
using System.Collections.Generic;
using System.Linq;

namespace Auxilia.Extensions
{
    /// <summary>
    /// Contains extensions for objects of type <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> when the given enumeration contains <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to check.</param>
        /// <param name="parameterName">Parameter name.</param>
        /// <returns>The given enumeration.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Throw when <paramref name="source"/> contains <see langword="null"/>.</exception>
        public static IEnumerable<T> ThrowIfNullOrContainsNull<T>(this IEnumerable<T> source, string parameterName)
        {
            source.ThrowIfNull(nameof(source));

            List<T> elements = source.ToList();
            return elements.Any(e => e == null)
                ? throw new ArgumentException("Cannot contain null.", parameterName)
                : elements;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> when the given enumeration is empty.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to check.</param>
        /// <param name="parameterName">Parameter name.</param>
        /// <returns>The given enumeration.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Throw when <paramref name="source"/> is empty.</exception>
        public static IEnumerable<T> ThrowIfEmpty<T>(this IEnumerable<T> source, string parameterName)
        {
	        source.ThrowIfNull(nameof(source));

	        List<T> elements = source.ToList();
            return elements.Any()
		        ? elements
                : throw new ArgumentException("Cannot be empty.", parameterName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> when the given enumeration is <see langword="null"/>, empty or contains <see langword="null"/>.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to check.</param>
        /// <param name="parameterName">Parameter name.</param>
        /// <returns>The given enumeration.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Throw when <paramref name="source"/> is <see langword="null"/>, empty or contains <see langword="null"/>.</exception>
        public static IEnumerable<T> ThrowIfNullEmptyOrContainsNull<T>(this IEnumerable<T> source, string parameterName)
        {
	        source.ThrowIfNull(nameof(source));

	        List<T> elements = source.ToList();
	        elements.ThrowIfEmpty(parameterName);
	        elements.ThrowIfNullOrContainsNull(parameterName);
	        return elements;
        }

        /// <summary>
        /// Checks if the given elements contain the specified element.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="element">Element to find.</param>
        /// <param name="elements">Elements to check.</param>
        /// <returns>True when <paramref name="elements"/> contain <paramref name="element"/>; false, otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="elements"/> is <see langword="null"/>.</exception>
        public static bool IsAnyOf<T>(this T element, params T[] elements)
        {
            elements.ThrowIfNull(nameof(elements));
            return elements.Contains(element);
        }

        /// <summary>
        /// Returns duplicates from the given enumeration.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to check.</param>
        /// <returns>Duplicates.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">Thrown when the number of elements in <paramref name="source"/> is larger than <see cref="Int32.MaxValue"/>.</exception>
        public static IEnumerable<T> Duplicates<T>(this IEnumerable<T> source)
        {
            source.ThrowIfNull(nameof(source));

            return source
                .GroupBy(i => i)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key);
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source)
        {
	        source.ThrowIfNull(nameof(source));
	        return source.Where(e => e != null);
        }

        /// <summary>
        /// Filters out the given values from the enumeration.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to filter.</param>
        /// <param name="elementsToRemove">Elements to remove.</param>
        /// <returns>Filtered values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, params T[] elementsToRemove)
        {
            source.ThrowIfNull(nameof(source));

            if (elementsToRemove == null)
            {
                foreach (T element in source.Where(e => e != null))
                    yield return element;
            }
            else
            {
                foreach (T element in source.Where(e => !elementsToRemove.Contains(e)))
                    yield return element;
            }
        }

        /// <summary>
        /// Orders the given values using the default comparer.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to order.</param>
        /// <returns>Ordered values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
		public static IEnumerable<T> Order<T>(this IEnumerable<T> source)
        {
            source.ThrowIfNull(nameof(source));
            return source.OrderBy(e => e);
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a set of values from the enumeration.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to trim.</param>
        /// <param name="trimElements">Elements to remove.</param>
        /// <returns>Trimmed values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/>.</exception>
        public static IEnumerable<T> Trim<T>(this IEnumerable<T> source, params T[] trimElements)
        {
            source.ThrowIfNull(nameof(source));

            return source.TrimStart(trimElements)
                .TrimEnd(trimElements);
        }

        /// <summary>
        /// Removes all leading occurrences of a set of values from the enumeration.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to trim.</param>
        /// <param name="trimElements">Elements to remove.</param>
        /// <returns>Trimmed values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static IEnumerable<T> TrimStart<T>(this IEnumerable<T> source, params T[] trimElements)
        {
            source.ThrowIfNull(nameof(source));

            bool trimmingStopped = false;

            foreach (T element in source)
            {
                if (trimElements == null)
                {
                    if (element == null)
                        trimmingStopped = true;
                }
                else
                {
                    if (!trimElements.Contains(element))
                        trimmingStopped = true;
                }

                if (trimmingStopped)
                    yield return element;
            }
        }

        /// <summary>
        /// Removes all tailing occurrences of a set of values from the enumeration.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to trim.</param>
        /// <param name="trimElements">Elements to remove.</param>
        /// <returns>Trimmed values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="trimElements"/> is <see langword="null"/>.</exception>
        public static IEnumerable<T> TrimEnd<T>(this IEnumerable<T> source, params T[] trimElements)
        {
            source.ThrowIfNull(nameof(source));

            List<T> elements = source.ToList();

            for (int i = elements.Count - 1; i >= 0; i--)
            {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
                if (!trimElements.Contains(elements[i]))
                    return elements.GetRange(0, i + 1);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
            }

            return Enumerable.Empty<T>();
        }

        /// <summary>
        /// Concatenates the given values.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="first">Enumeration to concatenate to.</param>
        /// <param name="elements">Elements to concatenate. Passing <see langword="null"/> will add <see langword="null"/> when the type is nullable.</param>
        /// <returns>Concatenated values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="first"/> is <see longword="null"/>.</exception>
        public static IEnumerable<T> ConcatenateElements<T>(this IEnumerable<T> first, params T[] elements)
        {
            first.ThrowIfNull(nameof(first));

            foreach (T element in first)
                yield return element;

            if (elements != null)
            {
                foreach (T element in elements)
                    yield return element;
            }
            else if (default(T) == null)
            {
                yield return default;
            }
        }

        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source, Random random)
        {
	        source.ThrowIfNull(nameof(source));
	        random.ThrowIfNull(nameof(random));

	        return source.OrderBy(_ => random.Next());
        }

        /// <summary>
        /// Concatenates two enumerations.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="first">First enumeration.</param>
        /// <param name="second">Second enumeration.</param>
        /// <returns>Concatenated values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="first"/> or <paramref name="second"/> is <see longword="null"/>.</exception>
        public static IEnumerable<T> Concatenate<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            first.ThrowIfNull(nameof(first));
            second.ThrowIfNull(nameof(second));

            foreach (T element in first)
                yield return element;

            foreach (T element in second)
                yield return element;
        }
        /// <summary>
        /// Concatenates values to an element.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="item">Item to concatenate to.</param>
        /// <param name="elements">Elements to concatenate.</param>
        /// <returns>Concatenated values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when values is <see langword="null"/>.</exception>
        public static IEnumerable<T> ConcatenateTo<T>(this T item, IEnumerable<T> elements)
        {
            elements.ThrowIfNull(nameof(elements));

            yield return item;

            foreach (T element in elements)
                yield return element;
        }

        /// <summary>
        /// Gets the string representations of the given values.
        /// The representation of <see langword="null"/> is an empty string.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to convert.</param>
        /// <returns>String representations of the values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static IEnumerable<string> SelectStrings<T>(this IEnumerable<T> source)
        {
            source.ThrowIfNull(nameof(source));
            return source.Select(s => $"{s}");
        }

        public static IEnumerable<T> ThrowIfContainsDuplicates<T>(this IEnumerable<T> source, string parameterName)
        {
	        List<T> elements = source.ToList();
	        List<T> duplicates = elements.Duplicates().ToList();

	        if (duplicates.Any())
		        throw new ArgumentException($"Contains duplicates: {duplicates.SelectStrings().Combine(", ")}");

	        return elements;
        }

        public static IEnumerable<T> Yield<T>(this T element)
        {
	        return new[] { element };
        }

        /// <summary>
        /// Combines the string representations of the given values.
        /// The representation of <see langword="null"/> is ignored.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to combine.</param>
        /// <param name="delimiter">String to be used between the string representations.</param>
        /// <returns>Combined string representation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="delimiter"/> is <see langword="null"/>.</exception>
        /// <exception cref="OutOfMemoryException">The length of the resulting string overflows the maximum allowed length <see cref="Int32.MaxValue"/>.</exception>
        public static string JoinStrings<T>(this IEnumerable<T> source, string delimiter = "")
        {
            source.ThrowIfNull(nameof(source));
            delimiter.ThrowIfNull(nameof(delimiter));

            return string.Join(delimiter, source.Except(null));
        }

        /// <summary>
        /// Concatenates the strings using the given separator.
        /// </summary>
        /// <param name="values">Strings to combine.</param>
        /// <param name="separator">String separator.</param>
        /// <returns>Concatenated string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="OutOfMemoryException">The length of the resulting string overflows the maximum allowed length <see cref="Int32.MaxValue"/>.</exception>
        public static string Combine(this IEnumerable<string> values, string separator = "")
        {
            values.ThrowIfNull(nameof(values));
            return string.Join(separator ?? string.Empty, values);
        }
        /// <summary>
        /// Concatenates the strings using the given separator.
        /// </summary>
        /// <param name="values">Strings to combine.</param>
        /// <param name="separator">String separator.</param>
        /// <returns>Concatenated string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="values"/> is <see langword="null"/>.</exception>
        /// <exception cref="OutOfMemoryException">The length of the resulting string overflows the maximum allowed length <see cref="Int32.MaxValue"/>.</exception>
        public static string Combine(this IEnumerable<string> values, char separator)
        {
            values.ThrowIfNull(nameof(values));
            return string.Join(separator.ToString(), values);
        }

        /// <summary>
        /// Returns the element that scores the highest given an evaluation.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to evaluate.</param>
        /// <param name="evaluation">Evaluation function that returns a value used to find the maximum.</param>
        /// <returns>First element with highest evaluation score.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="evaluation"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvocationException">Thrown when an error occurs while invoking the evaluation function.</exception>
        public static T MaxBy<T>(this IEnumerable<T> source, Func<T, double> evaluation)
        {
            source.ThrowIfNull(nameof(source));
            evaluation.ThrowIfNull(nameof(evaluation));

            List<T> elements = source.ToList();
            T result = elements.FirstOrDefault();
            double maxValue = double.MinValue;

            try
            {
                foreach (T element in elements)
                {
                    double value = evaluation(element);
                    if (value > maxValue)
                    {
                        result = element;
                        maxValue = value;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new InvocationException("An error occurred while invoking the evaluation function.", exception);
            }

            return result;
        }

        /// <summary>
        /// Returns the element that scores the lowest given an evaluation.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to evaluate.</param>
        /// /// <param name="evaluation">Evaluation function that returns a value used to find the minimum.</param>
        /// <returns>First element with lowest evaluation score.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="evaluation"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvocationException">Thrown when an error occurs while invoking the evaluation function.</exception>
        public static T MinBy<T>(this IEnumerable<T> source, Func<T, double> evaluation)
        {
            source.ThrowIfNull(nameof(source));
            evaluation.ThrowIfNull(nameof(evaluation));

            List<T> elements = source.ToList();
            T result = elements.FirstOrDefault();
            double minValue = double.MaxValue;

            try
            {
                foreach (T element in elements)
                {
                    double value = evaluation(element);
                    if (value < minValue)
                    {
                        result = element;
                        minValue = value;
                    }
                }
            }
            catch (Exception exception)
            {
                throw new InvocationException("An error occurred while invoking the evaluation function.", exception);
            }

            return result;
        }

        /// <summary>
        /// Count the number of occurrences of the given element.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to check.</param>
        /// <param name="element">Element to count.</param>
        /// <returns>Number of occurrences of the given element.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">Thrown when the number of elements in <paramref name="source"/> is larger than <see cref="Int32.MaxValue"/>.</exception>
        public static int Count<T>(this IEnumerable<T> source, T element)
        {
            source.ThrowIfNull(nameof(source));

            return element == null
                ? source.Count(e => e == null)
                : source.Count(e => e?.Equals(element) ?? false);
        }

        /// <summary>
        /// Checks if the given index is within the boundaries of the enumeration.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Enumeration to check.</param>
        /// <param name="index">Index to check.</param>
        /// <returns>True if the index is within the boundaries of the enumeration; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="OverflowException">Thrown when the number of elements in <paramref name="source"/> is larger than <see cref="Int32.MaxValue"/>.</exception>
        public static bool IsWithinBounds<T>(this IEnumerable<T> source, int index)
        {
            source.ThrowIfNull(nameof(source));
            return index >= 0 && index < source.Count();
        }

        /// <summary>
        /// Disposes all given elements.
        /// </summary>
        /// <param name="source">Elements to dispose.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvocationException">Thrown when an error occurs while invoking the action.</exception>
        public static void Dispose(this IEnumerable<IDisposable> source)
        {
            source.ThrowIfNull(nameof(source));
            source.Execute(e => e?.Dispose());
        }

        /// <summary>
        /// Invokes the given action for each element.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to be used as action arguments.</param>
        /// <param name="action">Action to be invoked.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
        public static void Execute<T>(this IEnumerable<T> source, Action<T> action)
        {
            source.ThrowIfNull(nameof(source));
            action.ThrowIfNull(nameof(action));

            foreach (T element in source)
                action(element);
        }

        /// <summary>
        /// Clones the given elements.
        /// </summary>
        /// <typeparam name="T">Element type.</typeparam>
        /// <param name="source">Elements to clone.</param>
        /// <returns>Cloned elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static IEnumerable<T> Clone<T>(this IEnumerable<ICloneable<T>> source)
        {
	        source.ThrowIfNull(nameof(source));

	        List<T> clones = new List<T>();
	        source.Execute(e => clones.Add(e.Clone()));
            return clones;
        }
    }
}
