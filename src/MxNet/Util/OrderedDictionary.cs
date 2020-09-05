using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace MxNet
{
    public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private OrderedDictionary _impl;

        public TValue this[TKey key]
        {
            get => (TValue)_impl[key];
            set => _impl[key] = value;
        }

        public ICollection<TKey> Keys => _impl.Keys.Cast<TKey>().ToArray();

        public ICollection<TValue> Values => _impl.Values.Cast<TValue>().ToArray();

        public int Count => _impl.Count;

        public bool IsReadOnly => _impl.IsReadOnly;

        public OrderedDictionary()
        {
            _impl = new OrderedDictionary();
        }

        public void Add(TKey key, TValue value)
        {
            _impl.Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _impl.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _impl.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _impl.Contains(item.Key);
        }

        public bool ContainsKey(TKey key)
        {
            return _impl.Contains(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            for (var i = arrayIndex; i < array.Length; ++i)
                _impl.Add(array[i].Key, array[i].Value);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(_impl.GetEnumerator());
        }

        public bool Remove(TKey key)
        {
            var result = _impl.Contains(key);
            _impl.Remove(key);
            return result;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_impl.Contains(key))
            {
                value = (TValue)_impl[key];
                return true;
            }
            value = default;
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            IDictionaryEnumerator _impl;

            public Enumerator(IDictionaryEnumerator impl)
            {
                _impl = impl;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get => new KeyValuePair<TKey, TValue>((TKey)_impl.Key, (TValue)_impl.Value);
            }

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
                // nothing to do
            }

            public bool MoveNext()
            {
                return _impl.MoveNext();
            }

            public void Reset()
            {
                _impl.Reset();
            }
        }
    }
}
