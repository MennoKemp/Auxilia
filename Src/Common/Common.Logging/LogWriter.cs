using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Timers;
using Common.Logging;

namespace Auxilia.Logging
{
    /// <summary>
    /// Class responsible for writing messages to a log file.
    /// </summary>
    internal class LogWriter
    {
        private static readonly Logger Logger = new Logger(nameof(LogWriter));
        private static readonly object LogLock = new object();

        private string _logDirectory;

        /// <summary>
        /// Initializes new instance of <see cref="LogWriter"/>.
        /// </summary>
        private LogWriter()
        {
            Timer clearTimer = null;

            try
            {
                LogDirectory = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().CodeBase.Remove("file:///")).DirectoryName, "Logs");
                DeleteOldLogs();

                clearTimer = new Timer();
                clearTimer.Elapsed += (sender, args) =>  DeleteOldLogs();
                clearTimer.Interval = new TimeSpan(1, 0, 0).TotalMilliseconds;
                clearTimer.Start();
            }
            catch(Exception exception)
            {
                clearTimer?.Dispose();
                Logger.LogCritical($"Cannot initialize {nameof(LogWriter)}.", exception);
                throw;
            }
        }

        /// <summary>
        /// Gets current instance of <see cref="LogWriter"/>.
        /// </summary>
        public static LogWriter Current { get; } = new LogWriter();

        /// <summary>
        /// Maximum duration [in days] that logs must be kept.
        /// The default is <see cref="uint.MaxValue"/>.
        /// </summary>
        public uint MaxLogAge { get; set; } = uint.MaxValue;
        /// <summary>
        /// Directory where the logs are saved.
        /// The default is %ExecutingAssemblyDirectory%\Logs.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is <see langword="null"/> or empty.</exception>
        public string LogDirectory
        {
            get => _logDirectory;
            set => _logDirectory = value.ThrowIfNullOrEmpty(nameof(value));
        }

        // TODO: Implement a queue to reduce file access.
        /// <summary>
        /// Writes the given log message to a file in the <see cref="LogDirectory"/>.
        /// </summary>
        /// <param name="logMessage">Message to log.</param>
        public void LogToFile(LogMessage logMessage)
        {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
            lock (LogLock)
            {
                try
                {
                    logMessage.ThrowIfNull(nameof(logMessage));

                    using StreamWriter logWriter = File.AppendText(GetLogFileName());
                    logWriter.WriteLine(logMessage);
                }
                catch (Exception exception)
                {
                    Logger.LogMessage(LogLevel.Critical, $"Cannot log: {logMessage}", exception, false);
                }
            }
#pragma warning restore ExceptionNotDocumented // Exception is not documented
        }

        /// <summary>
        /// Gets the log file name. The parent directory is created if it does not exist.
        /// </summary>
        /// <returns>Log file name.</returns>
        /// <exception cref="LogException">Thrown when an error occurs while getting the log file name.</exception>
        private string GetLogFileName()
        {
            string fileName = $"{DateTime.Now:yyyy-MM-dd}.log";

            try
            {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
                if (Directory.Exists(LogDirectory))
                    return Path.Combine(LogDirectory, fileName);

                Directory.CreateDirectory(LogDirectory);
                return Path.Combine(LogDirectory, fileName);
#pragma warning restore ExceptionNotDocumented // Exception is not documented
            }
            catch (Exception exception)
            {
                throw new LogException($"Cannot create log directory '{LogDirectory}'.", exception);
            }
        }

        /// <summary>
        /// Deletes old log files.
        /// </summary>
        private void DeleteOldLogs()
        {
            lock (LogLock)
            {
#pragma warning disable ExceptionNotDocumented // Exception is not documented
                try
                {
                    foreach (string logFile in Directory.GetFiles(LogDirectory, "*.log"))
                    {
                        if (DateTime.TryParseExact(
                            Path.GetFileNameWithoutExtension(logFile),
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.RoundtripKind,
                            out DateTime date))
                        {
                            if ((DateTime.Now - date).TotalDays > MaxLogAge)
                            {
                                try
                                {
                                    Logger.LogMessage(LogLevel.Info, $"Deleting old log '{logFile}'.", logToFile: false);
                                    File.Delete(logFile);
                                }
                                catch (Exception exception)
                                {
                                    throw new IOException($"Cannot delete old log '{logFile}'.", exception);
                                }
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    Logger.LogMessage(LogLevel.Error, "An error has occured while deleting old logs.", exception, false);
                }
#pragma warning restore ExceptionNotDocumented // Exception is not documented   
            }
        }
    }
}
