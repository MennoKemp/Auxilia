using System;

namespace Auxilia
{
    /// <summary>
    /// Contains extensions used for mathematical operations.
    /// </summary>
    public static class MathExtensions
    {
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this sbyte value, sbyte start, sbyte end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this byte value, byte start, byte end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this short value, short start, short end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this ushort value, ushort start, ushort end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this int value, int start, int end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this uint value, uint start, uint end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this long value, long start, long end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this ulong value, ulong start, ulong end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this float value, float start, float end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this double value, double start, double end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }
        /// <summary>
        /// Checks if a value is in between the given boundaries.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <param name="start">Lower boundary.</param>
        /// <param name="end">Upper boundary.</param>
        /// <param name="inclusive">Specifies whether the boundaries must be included in the range.</param>
        /// <returns>True if the value is within the range; otherwise, false.</returns>
        public static bool IsBetween(this decimal value, decimal start, decimal end, bool inclusive = false)
        {
            return inclusive
                ? value >= start && value <= end
                : value > start && value < end;
        }

        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static sbyte Clamp(this sbyte value, sbyte lowerLimit, sbyte upperLimit = sbyte.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static byte Clamp(this byte value, byte lowerLimit, byte upperLimit = byte.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static short Clamp(this short value, sbyte lowerLimit, short upperLimit = short.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static ushort Clamp(this ushort value, ushort lowerLimit, ushort upperLimit = ushort.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static int Clamp(this int value, int lowerLimit, int upperLimit = int.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static uint Clamp(this uint value, uint lowerLimit, uint upperLimit = uint.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static long Clamp(this long value, long lowerLimit, long upperLimit = long.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static ulong Clamp(this ulong value, ulong lowerLimit, ulong upperLimit = ulong.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static float Clamp(this float value, float lowerLimit, float upperLimit = float.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static double Clamp(this double value, double lowerLimit, double upperLimit = double.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }
        /// <summary>
        /// Clamps the given value between two limits.
        /// </summary>
        /// <param name="value">Value to clamp.</param>
        /// <param name="lowerLimit">Minimum return value.</param>
        /// <param name="upperLimit">Maximum return value.</param>
        /// <returns>The given value if it is between the limits; otherwise, the lower- or upper limit.</returns>
        public static decimal Clamp(this decimal value, decimal lowerLimit, decimal upperLimit = decimal.MaxValue)
        {
            return value < lowerLimit
                ? lowerLimit
                : value > upperLimit
                    ? upperLimit
                    : value;
        }

        /// <summary>
        /// Calculates the euclidean distance for 2 given numbers.
        /// </summary>
        /// <param name="a">Number 1.</param>
        /// <param name="b">Number 2.</param>
        /// <returns>Euclidean distance for the given numbers.</returns>
        public static double CalculateEuclideanDistance(this double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Calculates the euclidean distance for 3 given numbers.
        /// </summary>
        /// <param name="a">Number 1.</param>
        /// <param name="b">Number 2.</param>
        /// <param name="c">Number 3.</param>
        /// <returns>Euclidean distance for the given numbers.</returns>
        public static double CalculateEuclideanDistance(this double a, double b, double c)
        {
            return Math.Sqrt(a * a + b * b + c * c);
        }
    }
}