using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace Auxilia
{
	/// <summary>
	/// Queue that notifies when it's collection changes.
	/// </summary>
	/// <typeparam name="T">Element type.</typeparam>
	public class ObservableQueue<T> : Queue<T>, INotifyCollectionChanged
	{
		/// <summary>
		/// Occurs when the collection changes.
		/// </summary>
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Adds an object to the end of the <see cref="ObservableQueue{T}"/>.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="ObservableQueue{T}"/>. The value can be null for reference types.</param>
		public new void Enqueue(T item)
		{
			base.Enqueue(item);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
		}

		/// <summary>
		/// Removes and returns the object at the beginning of the <see cref="ObservableQueue{T}"/>.
		/// </summary>
		/// <returns>The object that is removed from the beginning of the <see cref="ObservableQueue{T}"/>.</returns>
		/// <exception cref="InvalidOperationException">Thrown when the <see cref="ObservableQueue{T}"/> is empty.</exception>
		public new T Dequeue()
		{
			T item = base.Dequeue();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
			return item;
		}

		/// <summary>
		/// Removes all objects from the <see cref="ObservableQueue{T}"/>.
		/// </summary>
		public new void Clear()
		{
			base.Clear();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		/// <summary>
		/// Removes the object at the beginning of the <see cref="ObservableQueue{T}"/>, and copies it to the result parameter.
		/// </summary>
		/// <param name="item">The removed object.</param>
		/// <returns>true if the object is successfully removed; false if the <see cref="ObservableQueue{T}"/> is empty.</returns>
		/// <exception cref="ArgumentNullException">Thrown when key is null.</exception>
		public new bool TryDequeue([MaybeNullWhen(false)] out T item)
		{
			bool result = base.TryDequeue(out item);

			if(result)
				CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));

			return result;
		}

	}
}
