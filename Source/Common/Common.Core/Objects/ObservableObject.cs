using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Auxilia
{
    /// <summary>
    /// Base class for objects which must implement <see cref="INotifyPropertyChanged"/>.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Triggers property notification.
        /// </summary>
        /// <param name="propertyName">The name of the changed property (optional).</param>
        /// <exception cref="InvocationException">Thrown when an error occurs while raising property changed.</exception>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch (Exception exception)
            {
                throw new InvocationException($"An error occurred while raising property changed for '{propertyName}'.", exception);
            }
        }

        /// <summary>
        /// Sets the value of a field and triggers property notification.
        /// If the given value is equal to the current value nothing will happen.
        /// </summary>
        /// <typeparam name="T">Field type.</typeparam>
        /// <param name="field">Field to be set.</param>
        /// <param name="value">Value to be set.</param>
        /// <param name="propertyName">Name of related property (optional).</param>
        /// <returns>True if the property has changed; false otherwise.</returns>
        /// <exception cref="InvocationException">Thrown when an error occurs while raising property changed.</exception>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return false;

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
