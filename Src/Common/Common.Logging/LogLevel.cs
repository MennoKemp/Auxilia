namespace Auxilia.Logging
{
    /// <summary>
    /// Type of log message.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Level to be used for lowest and most detailed messages.
        /// </summary>
        Trace = 0,
        /// <summary>
        /// Level to be used for debug messages.
        /// </summary>
        Debug = 1,
        /// <summary>
        /// Level to be used for happy-flow information messages.
        /// </summary>
        Info = 2,
        /// <summary>
        /// Level to be used for warning messages.
        /// </summary>
        Warning = 3,
        /// <summary>
        /// Level to be used for error logging.
        /// </summary>
        Error = 4,
        /// <summary>
        /// Level to be used for logging of unrecoverable errors.
        /// </summary>
        Critical = 5
    }
}