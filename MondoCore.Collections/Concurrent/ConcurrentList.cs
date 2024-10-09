using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MondoCore.Collections.Concurrent
{
    public class ConcurrentList<T> : IList<T>
    {
        private readonly List<T> _list = new();
        private readonly object  _lock = new();

        #region IList

        public T this[int index] 
        { 
            get 
            {             
                lock(_lock)
                    return _list[index];
            }

            set  
            { 
                lock(_lock)
                    _list[index] = value;
            }
        }

        public int Count        
        { 
            get 
            {             
                lock(_lock)
                    return _list.Count;
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            lock(_lock)
                _list.Add(item);
        }

        public void Clear()
        {
            lock(_lock)
                _list.Clear();
        }

        public bool Contains(T item)
        {
            lock(_lock)
                return _list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock(_lock)
                _list.CopyTo(array, arrayIndex);
        }

        public int IndexOf(T item)
        {
            lock(_lock)
                return _list.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            lock(_lock)
                _list.Insert(index, item);
        }

        public bool Remove(T item)
        {
            lock(_lock)
                return _list.Remove(item);
        }

        public void RemoveAt(int index)
        {
            lock(_lock)
                _list.RemoveAt(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ListEnumerator(_list, _lock);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Private

        private class ListEnumerator(List<T> list, object listLock) : IEnumerator<T>
        {
            private readonly IEnumerator<T> _enum = list.GetEnumerator();

            public T Current
            {
                get
                { 
                    lock(listLock)
                        return _enum.Current;
                }
            }

            object IEnumerator.Current
            {
                get
                { 
                    lock(listLock)
                        return _enum.Current;
                }
            }

            public void Dispose()
            {
                _enum.Dispose();
            }

            public bool MoveNext()
            {
                lock(listLock)
                    return _enum.MoveNext();
            }

            public void Reset()
            {
                lock(listLock)
                    _enum.Reset();
            }
        }

        #endregion
    }
}
