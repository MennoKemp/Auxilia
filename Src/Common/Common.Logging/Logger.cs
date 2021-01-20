using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Auxilia.Logging
{
    /// <summary>
    /// Use this class to log.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Initializes new instance of <see cref="Logger"/>.
        /// </summary>
        /// <param name="source">Name of the source of the message.</param>
        public Logger(string source)
        {
            Source = source.ThrowIfNullOrEmpty(nameof(source));
        }
        /// <summary>
        /// Initializes new instance of <see cref="Logger"/>.
        /// </summary>
        /// <param name="source">Source of the message.</param>
        public Logger(object source)
        {
            source.ThrowIfNull(nameof(source));
            // ReSharper disable once PossibleNullReferenceException
            Source = source.GetType().FullName.Split('.').Last();
        }

        /// <summary>
        /// Gets the source of the message.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Gets or sets the minimum log level for which messages will be logged.
        /// The default is <see cref="LogLevel.Info"/>.
        /// </summary>
        public static LogLevel Threshold { get; set; } = LogLevel.Info;

        /// <summary>
        /// Gets or sets the maximum duration [in days] that logs must be kept.
        /// The default is <see cref="uint.MaxValue"/>.
        /// </summary>
        public static uint MaxLogAge
        {
            get => LogWriter.Current.MaxLogAge;
            set => LogWriter.Current.MaxLogAge = value;
        }
        /// <summary>
        /// Gets or set directory where the logs are saved.
        /// The default is %ExecutingAssemblyDirectory%\Logs.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is <see langword="null"/> or empty.</exception>
        public string LogDirectory
        {
            get => LogWriter.Current.LogDirectory;
            set => LogWriter.Current.LogDirectory = value;
        }

        /// <summary>
        /// Message collection.
        /// </summary>
        public static ObservableCollection<LogMessage> Messages { get; } = new ObservableCollection<LogMessage>();

        /// <summary>
        /// Log a trace message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        /// <returns>Successful result based on the message.</returns>
        public Result LogTrace(string message)
        {
            return LogMessage(LogLevel.Trace, message);
        }
        /// <summary>
        /// Log a trace message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        /// <returns>Successful result based on the message.</returns>
        public Result LogDebug(string message)
        {
            return LogMessage(LogLevel.Debug, message);
        }
        /// <summary>
        /// Log an info message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        /// <returns>Successful result based on the message.</returns>
        public Result LogInfo(string message)
        {
            return LogMessage(LogLevel.Info, message);
        }
        /// <summary>
        /// Log an warning message.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="exception">Exception causing the warning (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        /// <returns>Successful result based on the message.</returns>
        public Result LogWarning(string message, Exception exception = null)
        {
            return LogMessage(LogLevel.Warning, message, exception);
        }
        /// <summary>
        /// Log an error.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="exception">Exception causing the error (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        /// <returns>Failed result based on the message.</returns>
        public Result LogError(string message, Exception exception = null)
        {
            return LogMessage(LogLevel.Error, message, exception);
        }
        /// <summary>
        /// Log a critical error.
        /// </summary>
        /// <param name="message">Message to log.</param>
        /// <param name="exception">Exception causing the error (optional).</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        /// <returns>Failed result based on the message.</returns>
        public Result LogCritical(string message, Exception exception = null)
        {
            return LogMessage(LogLevel.Critical, message, exception);
        }

        /// <summary>
        /// Logs a message by adding it to <see cref="Messages"/> and writing it to a log file.
        /// </summary>
        /// <param name="logLevel">Log level of message.</param>
        /// <param name="message">Message.</param>
        /// <param name="exception">Exception causing the message (optional).</param>
        /// <param name="logToFile">Specifies whether the message must be written to a log file (optional).</param>
        /// <returns>Successful result for non-error messages; a failed result, otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="message"/> is <see langword="null"/> or empty.</exception>
        internal Result LogMessage(LogLevel logLevel, string message, Exception exception = null, bool logToFile = true)
        {
            LogMessage logMessage = new LogMessage(Source, logLevel, message, exception);

            if (logLevel >= Threshold)
            {
                Messages.Add(logMessage);
                Debug.WriteLine(logMessage);

                if(logToFile)
                    LogWriter.Current.LogToFile(logMessage);
            }

            return logLevel <= LogLevel.Info
                ? Result.Successful(logMessage.Message)
                : Result.Failed(logMessage.Message, exception);
        }
    }
}
