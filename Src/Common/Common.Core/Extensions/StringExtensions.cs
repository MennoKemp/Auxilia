using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auxilia
{
    /// <summary>
    /// Contains extensions for objects of type <see cref="String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Throws a <see cref="ArgumentException"/> when the given <see cref="string"/> is <see langword="null"/> or empty.
        /// </summary>
        /// <param name="s">String to check.</param>
        /// <param name="parameterName">Name of the parameter used in the exception.</param>
        /// <returns>The given string.</returns>
        /// <exception cref="ArgumentException">Thrown when the given string is <see langword="null"/> or empty.</exception>
        public static string ThrowIfNullOrEmpty(this string s, string parameterName)
        {
            return string.IsNullOrEmpty(s)
                ? throw new ArgumentException("Cannot be null or empty.", parameterName)
                : s;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> when the given enumeration contains <see langword="null"/> or empty strings.
        /// </summary>
        /// <param name="source">Values to check.</param>
        /// <param name="parameterName">Name of the parameter used in the exception.</param>
        /// <returns>The given strings.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when the given enumeration contains <see langword="null"/> or empty strings.</exception>
        public static IEnumerable<string> ThrowIfContainsNullOrEmpty(this IEnumerable<string> source, string parameterName)
        {
            source.ThrowIfNull(nameof(source));

            List<string> elements = source.ToList();
            return elements.Any(string.IsNullOrEmpty)
                ? throw new ArgumentException("Cannot contain null or empty strings.", parameterName)
                : elements;
        }

        /// <summary>
        /// Checks if the given string is alpha numeric.
        /// Returns false when the string is <see langword="null"/> or empty.
        /// </summary>
        /// <param name="s">String to check.</param>
        /// <param name="ignoreWhitespace">Specifies whether whitespace must be ignored.</param>
        /// <returns>True if the string contains only alpha numeric characters; otherwise, false.</returns>
        public static bool IsAlphaNumeric(this string s, bool ignoreWhitespace)
        {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
            return s?.All(c => char.IsLetterOrDigit(c) ||
                               char.IsWhiteSpace(c) &&
                               ignoreWhitespace) ?? false;
#pragma warning restore ExceptionNotDocumented // Exception is not documented
        }

        /// <summary>
        /// Checks if the given string is numeric.
        /// Returns false when the string is <see langword="null"/> or empty.
        /// </summary>
        /// <param name="s">String to check.</param>
        /// <returns>True if the string is numeric; otherwise, false.</returns>
        public static bool IsNumeric(this string s)
        {
            try
            {
                return double.TryParse(s, out double _);
            }
            catch
            {
                return false;
            }
        }



        /// <summary>
        /// Counts the number of occurrences of a set of characters in the given string.
        /// </summary>
        /// <param name="s">String to check.</param>
        /// <param name="charactersToCount">Characters to count.</param>
        /// <returns>Number of occurrences of the character.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the given string is <see langword="null"/>.</exception>
        public static int Count(this string s, params char[] charactersToCount)
        {
            s.ThrowIfNull(nameof(s));

#pragma warning disable ExceptionNotDocumented // Exception is not documented
            return charactersToCount.Length == 1
                ? s.Count(c => c.Equals(charactersToCount[0]))
                : s.Count(charactersToCount.Contains);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
        }



        /// <summary>
        /// Returns the leftmost part of a string with the specified length
        /// or the complete string if the length of the string is larger than the length provided.
        /// </summary>
        /// <param name="s">String to crop.</param>
        /// <param name="length">Number of characters to keep, counting from the start.</param>
        /// <returns>Cropped string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the given length is smaller than zero.</exception>
        public static string Left(this string s, int length)
        {
            s.ThrowIfNull(nameof(s));

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "Cannot be negative.");

            return s.Substring(0, Math.Min(length, s.Length));
        }

        /// <summary>
        /// Returns the rightmost part of a string with the specified length
        /// or the complete string if the length of the string is larger than the length provided.
        /// </summary>
        /// <param name="s">String to crop.</param>
        /// <param name="length">Number of characters to keep, counting from the end.</param>
        /// <returns>Cropped string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the given length is smaller than zero.</exception>
        public static string Right(this string s, int length)
        {
            s.ThrowIfNull(nameof(s));

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "Cannot be negative.");

            length = Math.Min(length, s.Length);
            return s.Substring(s.Length - length, length);
        }

        /// <summary>
        /// Surrounds the given string with another string.
        /// </summary>
        /// <param name="s">String to surround with a text.</param>
        /// <param name="text">Text to add before and after the string.</param>
        /// <returns>Modified string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
        public static string SurroundWith(this string s, string text)
        {
            s.ThrowIfNull(nameof(s));
            text.ThrowIfNull(nameof(text));

            return text + s + text;
        }


        /// <summary>
        /// Filters the given string.
        /// </summary>
        /// <param name="s">String to filter.</param>
        /// <param name="charactersToKeep">Characters to keep.</param>
        /// <returns>Filtered string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <see cref="string"/> is <see langword="null"/>.</exception>
        public static string Keep(this string s, params char[] charactersToKeep)
        {
            s.ThrowIfNull(nameof(s));
            charactersToKeep.ThrowIfNull(nameof(charactersToKeep));
            return new string(s.Where(charactersToKeep.Contains).ToArray());
        }
        /// <summary>
        /// Filters the given string.
        /// </summary>
        /// <param name="s">String to filter.</param>
        /// <param name="filter">Function used to check if a character must be kept.</param>
        /// <returns>Filtered string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> or <paramref name="filter"/> is <see langword="null"/>.</exception>
        /// <exception cref="InvocationException">Thrown when an error occurs in the filter.</exception>
        public static string Keep(this string s, Func<char, bool> filter)
        {
            s.ThrowIfNull(nameof(s));
            filter.ThrowIfNull(nameof(filter));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in s)
            {
                try
                {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
                    if (filter(c))
                        stringBuilder.Append(c);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
                }
                catch (Exception exception)
                {
                    throw new InvocationException($"Filter threw an exception for '{c}'.", exception);
                }
            }

            return stringBuilder.ToString();
        }


        /// <summary>
        /// Filters the given string.
        /// </summary>
        /// <param name="s">String to filter.</param>
        /// <param name="textsToRemove">Texts to remove.</param>
        /// <returns>Filtered string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> or <paramref name="textsToRemove"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="textsToRemove"/> contains <see langword="null"/> or empty strings.</exception>
        public static string Remove(this string s, params string[] textsToRemove)
        {
            s.ThrowIfNull(nameof(s));
            textsToRemove.ThrowIfNull(nameof(textsToRemove));
            textsToRemove.ThrowIfContainsNullOrEmpty(nameof(textsToRemove));

#pragma warning disable ExceptionNotDocumented // Exception is not documented
            textsToRemove.Execute(t => s = s.Replace(t, string.Empty));
#pragma warning restore ExceptionNotDocumented // Exception is not documented

            return s;
        }
        /// <summary>
        /// Filters the given string.
        /// </summary>
        /// <param name="s">String to filter.</param>
        /// <param name="charactersToRemove">Characters to remove.</param>
        /// <returns>Filtered string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> or <paramref name="charactersToRemove"/> is <see langword="null"/>.</exception>
        public static string Remove(this string s, params char[] charactersToRemove)
        {
            s.ThrowIfNull(nameof(s));
            charactersToRemove.ThrowIfNull(nameof(charactersToRemove));
            return new string(s.Where(c => !charactersToRemove.Contains(c)).ToArray());
        }
        /// <summary>
        /// Filters the given string.
        /// </summary>
        /// <param name="s">String to filter.</param>
        /// <param name="filter">Function used to check if a character must be removed.</param>
        /// <returns>Filtered string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> or <paramref name="filter"/> is <see langword="null"/>.</exception>
        /// /// <exception cref="InvocationException">Thrown when an error occurs in the filter.</exception>
        public static string Remove(this string s, Func<char, bool> filter)
        {
            s.ThrowIfNull(nameof(s));
            filter.ThrowIfNull(nameof(filter));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in s)
            {
                try
                {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
                    if (!filter(c))
                        stringBuilder.Append(c);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
                }
                catch (Exception exception)
                {
                    throw new InvocationException($"Filter threw an exception for '{c}'.", exception);
                }
            }

            return stringBuilder.ToString();
        }



        /// <summary>
        /// Returns the strings that are not <see langword="null"/> or empty.
        /// </summary>
        /// <param name="source">Strings to filter.</param>
        /// <returns>Filtered strings.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <see langword="null"/>.</exception>
        public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string> source)
        {
            source.ThrowIfNull(nameof(source));
            return source.Where(s => !string.IsNullOrEmpty(s));
        }

        /// <summary>
        /// Splits the given string in segments of a specific maximum length.
        /// The last segment could have less characters than the segment size.
        /// </summary>
        /// <param name="s">String to split.</param>
        /// <param name="segmentSize">Maximum length of the segments.</param>
        /// <returns>An array containing the string segments.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when the segment size is smaller than 1.</exception>
        public static string[] Split(this string s, int segmentSize)
        {
            s.ThrowIfNull(nameof(s));

            if (segmentSize < 1)
                throw new ArgumentException("Segment size cannot be smaller than 1.");

            int segments = (int)Math.Ceiling((double)s.Length / segmentSize);

            string[] result = new string[segments];

            for (int i = 0; i < segments; i++)
            {
                int start = i * segmentSize;
                int length = Math.Min(s.Length - start, segmentSize);

#pragma warning disable ExceptionNotDocumented // Exception is not documented
                result[i] = s.Substring(start, length);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
            }

            return result;
        }

        /// <summary>
        /// Splits the given string into substring based on the given delimiter.
        /// </summary>
        /// <param name="s">String to split.</param>
        /// <param name="delimiter">Substring delimiter.</param>
        /// <param name="count">Maximum number of substrings.</param>
        /// <returns>An array containing the string segments.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="s"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="delimiter"/> is <see langword="null"/> or empty.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="count"/> is negative.</exception>
        public static string[] Split(this string s, string delimiter, int count = int.MaxValue)
        {
            s.ThrowIfNull(nameof(s));
            delimiter.ThrowIfNullOrEmpty(nameof(delimiter));

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Cannot be negative.");

            return s.Split(new[] { delimiter }, count, StringSplitOptions.None);
        }

        /// <summary>
        /// Converts the string representation of a number to its double-precision floating-point number equivalent.
        /// </summary>
        /// <param name="s">String containing a number to convert.</param>
        /// <returns>The number equivalent if successful; otherwise 0.</returns>
        public static double ToDouble(this string s)
        {
            double.TryParse(s, out double result);
            return result;
        }

        /// <summary>
        /// Converts the string representation of a number to its 32-bit signed number equivalent.
        /// </summary>
        /// <param name="s">String containing a number to convert.</param>
        /// <returns>The number equivalent if successful; otherwise 0.</returns>
        public static int ToInteger(this string s)
        {
            int.TryParse(s, out int result);
            return result;
        }
    }
}
