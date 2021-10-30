namespace Auxilia.Utilities
{
    /// <summary>
    /// Supported special setting directories.
    /// </summary>
    public enum SettingsDirectoryType
    {
        /// <summary>
        /// Use the settings directory from <see cref="ApplicationSettings.Path"/>.
        /// </summary>
        Custom,
        /// <summary>
        /// Local application data directory.
        /// When <see cref="ApplicationSettings.Path"/> is null or empty,
        /// the application name will be used.
        /// </summary>
        LocalApplicationData,
        /// <summary>
        /// Roaming application data directory.
        /// When <see cref="ApplicationSettings.Path"/> is null or empty,
        /// the application name will be used.
        /// </summary>
        RoamingApplicationData
    }
}
