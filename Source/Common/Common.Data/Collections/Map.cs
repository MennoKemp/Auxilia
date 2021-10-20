using Auxilia.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Auxilia.Data.Collections
{
	public class Map<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
    {
        private Dictionary<T1, T2> _forward;
        private Dictionary<T2, T1> _reverse;

        public Map()
            : this(Enumerable.Empty<KeyValuePair<T1, T2>>(), null, null)
        {
        }
        public Map(IEnumerable<KeyValuePair<T1, T2>> values)
            : this(values, null, null)
        {
        }
        public Map(
            IEqualityComparer<T1> forwardComparer,
            IEqualityComparer<T2> reverseComparer)
            : this(Enumerable.Empty<KeyValuePair<T1, T2>>(), forwardComparer, reverseComparer)
        {

        }
        public Map(
            IEnumerable<KeyValuePair<T1, T2>> values, 
            IEqualityComparer<T1> forwardComparer, 
            IEqualityComparer<T2> reverseComparer)
        {
            values.ThrowIfNullOrContainsNull(nameof(values));

            _forward = new Dictionary<T1, T2>(forwardComparer);
            _reverse = new Dictionary<T2, T1>(reverseComparer);

            values.Execute(v =>
            {
                _forward.Add(v.Key, v.Value);
                _reverse.Add(v.Value, v.Key);
            });

            Forward = new Indexer<T1, T2>(_forward, _reverse);
            Reverse = new Indexer<T2, T1>(_reverse, _forward);
        }

        public Indexer<T1, T2> Forward { get; }
        public Indexer<T2, T1> Reverse { get; }

        public int Count
        {
            get => _forward.Count;
        }

        public void Add(T1 value1, T2 value2)
        {
            if (Forward.Contains(value1))
                throw new ArgumentException($"Map already contains value \"{value1}\".");

            if (Reverse.Contains(value2))
                throw new ArgumentException($"Map already contains value \"{value2}\".");

            _forward.Add(value1, value2);
            _reverse.Add(value2, value1);
        }

        public bool Remove(T1 value)
        {
            return Forward.Contains(value) && 
                _reverse.Remove(Forward[value]) &&
                _forward.Remove(value);
        }
        public bool Remove(T2 value)
        {
            return Reverse.Contains(value) &&
                _forward.Remove(Reverse[value]) &&
                _reverse.Remove(value);
        }

        public void Clear()
        {
            _forward.Clear();
            _reverse.Clear();
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return _forward.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _forward.GetEnumerator();
        }

        public class Indexer<TKey, TValue>
        {
            private readonly Dictionary<TKey, TValue> _forward;
            private readonly Dictionary<TValue, TKey> _reverse;

            internal Indexer(Dictionary<TKey, TValue> forward, Dictionary<TValue, TKey> reverse)
            {
                _forward = forward;
                _reverse = reverse;
            }

            public TValue this[TKey key]
            {
                get => _forward[key];
                set
                {
                    if(_forward.TryGetValue(key, out TValue existingValue) && existingValue.Equals(value))
                        return;
                    
                    if (_reverse.ContainsKey(value))
                        throw new ArgumentException($"Map already contains value \"{value}\".", nameof(value));

                    _reverse.Remove(existingValue);
                    _forward[key] = value;
                    _reverse[value] = key;
                }
            }

            public bool Contains(TKey key)
            {
                return _forward.ContainsKey(key);
            }

            public IEnumerable<TKey> Keys
            {
                get => _forward.Keys;
            }

            public IEnumerable<TValue> Values
            {
                get => _forward.Values;
            }

            public bool TryGetValue(TKey key, out TValue value)
            {
                return _forward.TryGetValue(key, out value);
            }
        }
    }
}
