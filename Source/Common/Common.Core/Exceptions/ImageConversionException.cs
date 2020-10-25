using System;

namespace Auxilia
{
    /// <summary>
    /// Common exception for image conversion errors.
    /// </summary>
    public class ImageConversionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ImageConversionException"/>.
        /// </summary>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ImageConversionException(Exception innerException)
            : base("An error occurred while converting an image.", innerException)
        {
        }
    }
}