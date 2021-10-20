using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Auxilia.Utilities
{
    /// <summary>
    /// Base class for application settings.
    /// </summary>
    public abstract class ApplicationSettings
    {
        ///// <summary>
        ///// Initializes new instance of <see cref="ApplicationSettings"/>.
        ///// </summary>
        ///// <param name="settingsDirectory">Directory in which the settings are stored.</param>
        //protected ApplicationSettings(string settingsDirectory)
        //{
        //    SettingsFolderName = settingsDirectory;
        //}
        ///// <summary>
        ///// Initializes new instance of <see cref="ApplicationSettings"/>.
        ///// </summary>
        ///// <param name="settingsDirectory">CommandName of the folder in which the settings are stored.</param>
        //protected ApplicationSettings(string settingsFolderName, bool use)
        //{
        //    SettingsFolderName = settingsDirectory;
        //}
        
        /// <summary>
        /// Gets the type of settings directory.
        /// </summary>
        protected virtual SettingsDirectoryType SettingsDirectoryType { get; }
        /// <summary>
        /// Gets or sets the custom settings directory.
        /// Use an absolute path for <see cref="SettingsDirectoryType.Custom"/>
        /// and a relative path for <see cref="SettingsDirectoryType.LocalApplicationData"/> or <see cref="SettingsDirectoryType.LocalApplicationData"/>.
        /// </summary>
        protected virtual string CustomSettingsDirectory { get; set; }
        
        /// <summary>
        /// Gets the name of the application settings file.
        /// </summary>
        protected virtual string FileName { get; } = "AppSettings.txt";
        /// <summary>
        /// Gets the encoding for the settings file.
        /// </summary>
        protected virtual Encoding Encoding { get; } = Encoding.UTF8;

        ///// <summary>
        ///// Gets the value for a specific setting.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="currentSettings"></param>
        ///// <param name="defaultValue"></param>
        ///// <param name="settingName"></param>
        ///// <returns></returns>
        ///// <exception cref="ArgumentNullException">Thrown when <paramref name="currentSettings"/> is <see langword="null"/>.</exception>
        ///// <exception cref="ArgumentException">Thrown when <paramref name="settingName"/> is <see langword="null"/> or empty.</exception>
        //protected static T GetSetting<T>(ApplicationSettings currentSettings, T defaultValue = default, [CallerMemberName] string settingName = "")
        //{
        //    currentSettings.ThrowIfNull(nameof(currentSettings));
        //    settingName.ThrowIfNullOrEmpty(nameof(settingName));

        //    try
        //    {
        //        List<Setting> settings = currentSettings.GetSettings();

        //        if (settings.SingleOrDefault(s => s.CommandName.Equals(settingName, StringComparison.OrdinalIgnoreCase)) is Setting setting)
        //            return (T)Convert.ChangeType(setting.Value, typeof(T));

        //        settings.Add(new Setting(settingName, defaultValue?.ToString() ?? string.Empty));
        //        currentSettings.SaveSettings(settings);
        //        return defaultValue;
        //    }
        //    catch(Exception exception)
        //    {
        //        throw new SettingsException($"An error occured while getting setting '{settingName}'.", exception);
        //    }
        //}

        //protected static void SetSetting<T>(ApplicationSettings currentSettings, T value, [CallerMemberName] string settingName = "")
        //{
        //    settingName.ThrowIfNullOrEmpty(nameof(settingName));

        //    try
        //    {
        //        List<Setting> settings = currentSettings.GetSettings();

        //        if (settings.SingleOrDefault(s => s.CommandName.Equals(settingName, StringComparison.OrdinalIgnoreCase)) is Setting setting)
        //            setting.Value = value?.ToString() ?? string.Empty;

        //        currentSettings.SaveSettings(settings);
        //    }
        //    catch(Exception exception)
        //    {
        //        throw new SettingsException($"An error occured while setting '{settingName}'.", exception);
        //    }
        //}

        //private string GetSettingsPath()
        //{
        //    Root.ThrowIfNullOrEmpty(nameof(Root));
        //    SettingsFolderName.ThrowIfNullOrEmpty(nameof(SettingsFolderName));
        //    FileName.ThrowIfNullOrEmpty(nameof(FileName));

        //    PathInfo pathInfo = new PathInfo(Root, SettingsFolderName, FileName);
            
        //    if(!pathInfo.IsPathValid().IsSuccessful(out Result result))
        //        throw new InvalidOperationException(result.Message);

        //    return pathInfo.FullPath;
        //}

        //private void SaveSettings(IEnumerable<Setting> settings)
        //{
        //    try
        //    {
        //        File.WriteAllLines(GetSettingsPath(), settings.SelectStrings(), Encoding);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new SettingsException($"An error occurred while saving settings to '{GetSettingsPath()}'.", exception);
        //    }
        //}

        //private List<Setting> GetSettings()
        //{
        //    string settingsPath = GetSettingsPath();

        //    try
        //    {
        //        if (!File.Exists(settingsPath))
        //        {
        //            string settingsFolder = new PathInfo(settingsPath).Parent.FullPath;

        //            if (!Directory.Exists(settingsFolder))
        //                Directory.CreateDirectory(settingsFolder);

        //            File.Create(settingsPath).Dispose();
        //        }

        //        return File.ReadAllLines(settingsPath)
        //            .Select(l => l.Split('='))
        //            .Where(l => l.Length == 2)
        //            .Select(l => new Setting(l[0], l[1]))
        //            .ToList();
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new SettingsException($"An error occurred while getting settings from '{GetSettingsPath()}'.", exception);
        //    }
        //}
    }
}
