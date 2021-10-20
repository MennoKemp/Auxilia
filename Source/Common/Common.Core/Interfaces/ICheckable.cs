using System.ComponentModel;

namespace Auxilia
{
	/// <summary>
	/// Interface to be used for data grid objects where a check box column is used to (un)check properties.
	/// </summary>
	public interface ICheckable : INotifyPropertyChanged
	{
		/// <summary>
		/// Sets the value of a <see cref="bool"/> property.
		/// </summary>
		/// <param name="propertyName">Name of the property to set.</param>
		/// <param name="value">Value to set.</param>
		void SetBoolProperty(string propertyName, bool value);

		/// <summary>
		/// Gets the value of a <see cref="bool"/> property.
		/// </summary>
		/// <param name="propertyName">Name of the property to get.</param>
		/// <returns>Value of the property.</returns>
		bool GetBoolProperty(string propertyName);
	}
}
