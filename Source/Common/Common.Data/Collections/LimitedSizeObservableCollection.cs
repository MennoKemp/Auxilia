using Auxilia.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Auxilia.Data.Collections
{
	public class LimitedSizeObservableCollection<T> : INotifyCollectionChanged, ICollection<T>
	{
		private readonly IList<T> _collection;

		public LimitedSizeObservableCollection(int capacity)
			: this(capacity, Enumerable.Empty<T>())
		{
		}

		public LimitedSizeObservableCollection(int capacity, IEnumerable<T> elements)
		{
			Capacity = capacity.ThrowIfOutOfRange(nameof(capacity), 1);
			_collection = new List<T>(elements);
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public int Capacity { get; }

		public void Add(T item)
		{
			if(_collection.Count == Capacity)
				_collection.RemoveAt(0);

			_collection.Add(item);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new[] {item}));
		}

		public void Clear()
		{
			_collection.Clear();
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		public bool Contains(T item)
		{
			return _collection.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			_collection.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			bool result = _collection.Remove(item);
			CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new [] {item}));
			return result;
		}

		public int Count
		{
			get => _collection.Count;
		}
		public bool IsReadOnly { get; } = false;

		public IEnumerator<T> GetEnumerator()
		{
			return _collection.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _collection.GetEnumerator();
		}
	}
}
