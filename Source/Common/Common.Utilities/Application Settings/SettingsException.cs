using System;

namespace Auxilia.Utilities
{
    /// <summary>
    /// Exception thrown when an error related to settings occurs.
    /// </summary>
    public class SettingsException : Exception
    {
        /// <summary>
        /// Initializes new instance of <see cref="SettingsException"/>.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public SettingsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
