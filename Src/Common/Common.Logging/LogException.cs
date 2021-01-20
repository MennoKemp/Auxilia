using System;

namespace Common.Logging
{
    /// <summary>
    /// Exception thrown when an error occurs during logging.
    /// </summary>
    public class LogException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LogException"/>.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public LogException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of <see cref="LogException"/>.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public LogException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
