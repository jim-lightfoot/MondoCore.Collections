using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MondoCore.Collections.Internal
{
    internal class ReadOnlyDictionaryCollection<K, V> : IReadOnlyDictionary<K, V>
    {
        private List<IReadOnlyDictionary<K, V>> _dictionaries = new();

        public void Add(IReadOnlyDictionary<K, V> dict)
        {
            if(dict is ReadOnlyDictionaryCollection<K, V> coll)
                _dictionaries.AddRange(coll._dictionaries);
            else
                _dictionaries.Add(dict);
        }

        #region IReadOnlyDictionary

        public V this[K key] 
        {
            get
            {
                foreach(var dict in _dictionaries) 
                {
                    if(dict.ContainsKey(key))
                        return dict[key];
                }

                throw new KeyNotFoundException($"A value with the key name of '{key}' was not found");
            }
        }

        public IEnumerable<K> Keys
        {
            get
            {
                var keys = new Dictionary<K, K>();

                foreach(var dict in _dictionaries) 
                {
                    foreach(var key in dict.Keys) 
                    {
                        if(!keys.ContainsKey(key))
                            keys.Add(key, key);
                    }
                }

                return keys.Keys;
            }
        }

        public IEnumerable<V> Values
        {
            get
            {
                var values = new List<V>();

                foreach(var dict in _dictionaries) 
                {
                    foreach(var val in dict.Values) 
                    {
                        values.Add(val);
                    }
                }

                return values;
            }
        }

        public int Count
        {
            get
            {
                var count = 0;

                foreach(var dict in _dictionaries) 
                    count += dict.Count;

                return count;
            }
        }

        public bool ContainsKey(K key)
        {
            foreach(var dict in _dictionaries) 
            {
                if(dict.ContainsKey(key))   
                    return true;
            }
            return false;
        }

        public bool TryGetValue(K key, out V value)
        {
            foreach(var dict in _dictionaries) 
            {
                if(dict.TryGetValue(key, out value))   
                    return true;
            }

            value = default;
            return false;
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return new MyEnumerator(_dictionaries);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Private

        private class MyEnumerator(List<IReadOnlyDictionary<K, V>> list) : IEnumerator<KeyValuePair<K, V>>
        {
            private int _index = 0;
            private IEnumerator<KeyValuePair<K, V>>? _currentEnum = list.FirstOrDefault()?.GetEnumerator();

            public KeyValuePair<K, V> Current => _currentEnum!.Current;

            object IEnumerator.Current => _currentEnum!.Current;

            public void Dispose()
            {
                _currentEnum?.Dispose();
                _currentEnum = null;
            }

            public bool MoveNext()
            {
                if(_currentEnum?.MoveNext() ?? false)
                    return true;

                if(++_index >= list.Count) 
                    return false;

                _currentEnum = list[_index].GetEnumerator();

                _currentEnum.MoveNext();

                return true;
            }

            public void Reset()
            {
                _currentEnum?.Reset();
                _index = 0;
                _currentEnum = list.FirstOrDefault()?.GetEnumerator();
            }
        }

        #endregion
    }
}
