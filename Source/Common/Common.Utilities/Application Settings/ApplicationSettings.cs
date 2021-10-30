using Auxilia.Extensions;
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
	/// Handles retrieving and storing of settings.
	/// </summary>
	public abstract class ApplicationSettings
    {
        /// <summary>
        /// Gets the settings directory type.
        /// </summary>
        protected abstract SettingsDirectoryType SettingsDirectoryType { get; }
        /// <summary>
        /// Gets the path for the settings file.
        /// Use an absolute path for <see cref="SettingsDirectoryType.Custom"/>
		/// and a relative path for the other types.
        /// </summary>
        protected abstract string Path { get; }
        
        /// <summary>
        /// Gets the name of the application settings file.
		/// Default value: "AppSettings.txt".
		/// </summary>
        protected virtual string FileName { get; } = "AppSettings.txt";
        /// <summary>
        /// Gets the encoding for the settings file.
		/// Devault value: <see cref="Encoding.UTF8"/>.
        /// </summary>
        protected virtual Encoding Encoding { get; } = Encoding.UTF8;

		/// <summary>
		/// Gets the value for the specified setting.
		/// </summary>
		/// <typeparam name="T">Setting type.</typeparam>
		/// <param name="currentSettings">Instance of <see cref="ApplicationSettings"/>.</param>
		/// <param name="defaultValue">Default value in case the setting does not exist.</param>
		/// <param name="settingName">Name of the setting.</param>
		/// <returns>The setting value or the default value if it does not exist.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="currentSettings"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="settingName"/> is <see langword="null"/> or empty.</exception>
		/// <exception cref="SettingsException">Thrown when retrieving of a setting failed.</exception>
		protected static T GetSetting<T>(ApplicationSettings currentSettings, T defaultValue = default, [CallerMemberName] string settingName = "")
		{
			currentSettings.ThrowIfNull(nameof(currentSettings));
			settingName.ThrowIfNullOrEmpty(nameof(settingName));

			try
			{
				List<Setting> settings = currentSettings.GetSettings();

				if (settings.SingleOrDefault(s => s.Name.Equals(settingName, StringComparison.OrdinalIgnoreCase)) is Setting setting)
					return (T)Convert.ChangeType(setting.Value, typeof(T));

				settings.Add(new Setting(settingName, defaultValue?.ToString() ?? string.Empty));
				currentSettings.SaveSettings(settings);
				return defaultValue;
			}
			catch (Exception exception)
			{
				throw new SettingsException($"An error occured while getting setting '{settingName}'.", exception);
			}
		}

		/// <summary>
		/// Sets a value for the specified setting.
		/// </summary>
		/// <typeparam name="T">Setting type.</typeparam>
		/// <param name="currentSettings">Instance of <see cref="ApplicationSettings"/></param>
		/// <param name="value">Value to set.</param>
		/// <param name="settingName">Name of the setting.</param>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="currentSettings"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">Thrown when <paramref name="settingName"/> is <see langword="null"/> or empty or contains "=" 
		/// or when <see cref="Path"/> is invalid.</exception>
		/// <exception cref="SettingsException">Thrown when storing of a setting failed.</exception>
		protected static void SetSetting<T>(ApplicationSettings currentSettings, T value, [CallerMemberName] string settingName = "")
		{
			currentSettings.ThrowIfNull(nameof(currentSettings));
			settingName.ThrowIfNullOrEmpty(nameof(settingName));

			if (settingName.Contains('='))
				throw new ArgumentException("Cannot contain \"=\".", nameof(settingName));

			try
			{
				List<Setting> settings = currentSettings.GetSettings();

				if (settings.SingleOrDefault(s => s.Name.Equals(settingName, StringComparison.OrdinalIgnoreCase)) is Setting setting)
					setting.Value = $"{value}";

				currentSettings.SaveSettings(settings);
			}
			catch (Exception exception)
			{
				throw new SettingsException($"An error occured while setting '{settingName}'.", exception);
			}
		}

		private string GetSettingsPath()
		{
			Path.ThrowIfNullOrEmpty(nameof(Path));

			PathInfo path = SettingsDirectoryType switch
			{
				SettingsDirectoryType.Custom => new PathInfo(Path),
				SettingsDirectoryType.LocalApplicationData => new PathInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Path),
				SettingsDirectoryType.RoamingApplicationData => new PathInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Path),
				_ => throw new EnumValueNotDefinedException<SettingsDirectoryType>(SettingsDirectoryType, nameof(SettingsDirectoryType))
			};

			if(path.IsValid(SettingsDirectoryType == SettingsDirectoryType.Custom))
				throw new ArgumentException($"Path {path} is invalid.");

			return path.FullPath;
		}

		private void SaveSettings(IEnumerable<Setting> settings)
		{
			try
			{
				PathInfo settingsPath = new PathInfo(GetSettingsPath());
				settingsPath.Parent.Create();

				File.WriteAllLines(GetSettingsPath(), settings.SelectStrings(), Encoding);
			}
			catch (Exception exception)
			{
				throw new SettingsException($"An error occurred while saving settings to '{GetSettingsPath()}'.", exception);
			}
		}

		private List<Setting> GetSettings()
		{
			string settingsPath = GetSettingsPath();

			try
			{
				return File.Exists(settingsPath)
					? File.ReadAllLines(settingsPath)
					.Select(l => l.Split('='))
					.Where(l => l.Length == 2)
					.Select(l => new Setting(l[0], l[1]))
					.ToList()
					: new List<Setting>();
			}
			catch (Exception exception)
			{
				throw new SettingsException($"An error occurred while getting settings from '{settingsPath}'.", exception);
			}
		}
	}
}
