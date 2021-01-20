using System;

namespace Auxilia.Logging
{
    /// <summary>
    /// Wrapper class for log messages. Contains the message, log level, timestamp and other information.
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// Initializes new instance of <see cref="LogLevel"/>
        /// </summary>
        /// <param name="source">Name of the source of the message.</param>
        /// <param name="logLevel">Log level of the message.</param>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception causing the message (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="source"/> or <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        internal LogMessage(string source, LogLevel logLevel, string message, Exception exception = null)
        {
            Source = source.ThrowIfNullOrEmpty(nameof(source));
            LogLevel = logLevel;
            Message = message.ThrowIfNullOrEmpty(nameof(message));

            if (exception != null)
                Message += Environment.NewLine + exception;
        }

        /// <summary>
        /// Gets the time stamp of the message.
        /// </summary>
        public DateTime TimeStamp { get; } = DateTime.Now;
        /// <summary>
        /// Gets the source of the message.
        /// </summary>
        public string Source { get; }
        /// <summary>
        /// Gets the log level of the message.
        /// </summary>
        public LogLevel LogLevel { get; }
        /// <summary>
        /// Gets the log message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Returns object string.
        /// </summary>
        /// <returns>Log message containing the time stamp, log level, source and message.</returns>
        public override string ToString()
        {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
            return $"{TimeStamp:yyyy-MM-dd HH:mm:ss:fff}\t" +
                   $"{LogLevel,-10}\t" +
                   $"{Source.PadRight(40).Substring(0, 40)}\t" +
                   Message;
#pragma warning restore ExceptionNotDocumented // Exception is not documented
        }
    }
}