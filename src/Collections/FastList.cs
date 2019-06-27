using Extras.Diagnostics;
using Extras.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Extras.Collections
{
    [Serializable]
    public class FastList<T> : IList<T>
    {
        public int Count => _count;

        public int Capacity => _capacity;

        public T this [int index]
        {
            get
            {
                Guard.True(index >= _count);
                return _items[index];
            }
            set
            {
                Guard.True(index >= _count);
                _items[index] = value;
            }
        }

        public FastList() : this(null) { }

        public FastList(EqualityComparer<T> comparer) : this(INIT_CAPACITY, comparer) { }

        public FastList(int capacity, EqualityComparer<T> comparer = null)
        {
            var type = typeof(T);
            _isNullable = !type.IsValueType || Nullable.GetUnderlyingType(type).IsNotNull();
            _capacity = capacity > INIT_CAPACITY ? capacity : INIT_CAPACITY;
            _count = 0;
            _comparer = comparer;
            _items = new T[_capacity];
        }

        public void Add (T item)
        {
            if (_count == _capacity)
            {
                if (_capacity > 0)
                {
                    _capacity <<= 1;
                }
                else
                {
                    _capacity = INIT_CAPACITY;
                }
                var items = new T[_capacity];

                Array.Copy(_items, items, _count);
                _items = items;
            }

            _items[_count] = item;
            _count++;
        }

        public void AddRange(IEnumerable<T> data)
        {
            Guard.NotNull(data, $"{nameof(data)} is null");

            if (data is ICollection<T> collection)
            {
                var amount = collection.Count;

                if (amount <= 0)
                {
                    return;
                }

                Reserve(amount, false, false);
                collection.CopyTo(_items, _count);
                _count += amount;
            }
            else
            {
                using (var enumerator = data.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Add(enumerator.Current);
                    }
                }
            }
        }

        public void AssignData(T[] data, int count)
        {
            _items = data ?? throw new ArgumentNullException(nameof(data));
            _count = count >= 0 ? count : 0;
            _capacity = _items.Length;
        }

        public void Clear() => Clear(false);

        public void Clear(bool forceSetDefaultValues = false)
        {
            if (_isNullable || forceSetDefaultValues)
            {
                for (var i = _count - 1; i >= 0; i--)
                {
                    _items[i] = default;
                }
            }
            _count = 0;
        }

        public bool Contains(T item) => IndexOf(item) != -1;

        public void CopyTo(T[] array, int arrayIdx) => Array.Copy(_items, 0, array, arrayIdx, _count);

        public void FillWithEmpty(
            int amount,
            bool clearCollection = false,
            bool forceSetDefaultValues = true)
        {
            if (amount > 0)
            {
                if (clearCollection)
                {
                    _count = 0;
                }

                Reserve(amount, clearCollection, forceSetDefaultValues);
                _count += amount;
            }
        }

        public int IndexOf(T item)
        {
            int idx;

            if (_comparer.IsNotNull())
            {
                for (idx = _count - 1; idx >= 0; idx--)
                {
                    if (_comparer.Equals (_items[idx], item))
                    {
                        break;
                    }
                }
            }
            else
            {
                idx = Array.IndexOf (_items, item, 0, _count);
            }

            return idx;
        }

        public void Insert(int idx, T item)
        {
            if (idx >= 0 && idx < _count)
            {
                Reserve(1, false, false);
                Array.Copy(_items,
                           idx,
                           _items,
                           idx + 1,
                           _count - idx
                );
                _items[idx] = item;
                _count++;
            }
        }

        public bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator() => throw new NotSupportedException ();

        IEnumerator IEnumerable.GetEnumerator() => throw new NotSupportedException ();

        public bool Remove(T item)
        {
            var id = IndexOf (item);
            if (id == -1) {
                return false;
            }
            RemoveAt (id);

            return true;
        }

        public void RemoveAt(int idx)
        {
            if (idx >= 0 && idx < _count)
            {
                _count--;
                Array.Copy(_items,
                           idx + 1,
                           _items,
                           idx,
                           _count - idx
                );
            }
        }

        public bool RemoveLast(bool forceSetDefaultValues = true)
        {
            if (_count > 0)
            {
                _count--;
                if (forceSetDefaultValues)
                {
                    _items[_count] = default;
                }

                return true;
            }

            return false;
        }

        public void Reserve(
            int amount,
            bool totalAmount = false,
            bool forceSetDefaultValues = true)
        {
            if (amount > 0)
            {
                var start = totalAmount ? 0 : _count;
                var newCount = start + amount;

                if (newCount > _capacity)
                {
                    if (_capacity <= 0)
                    {
                        _capacity = INIT_CAPACITY;
                    }

                    while (_capacity < newCount)
                    {
                        _capacity <<= 1;
                    }

                    var items = new T[_capacity];

                    Array.Copy(_items, items, _count);
                    _items = items;
                }

                if (forceSetDefaultValues)
                {
                    for (var i = _count; i < newCount; i++)
                    {
                        _items[i] = default;
                    }
                }
            }
        }

        public void Reverse()
        {
            if (_count > 0)
            {
                for (int idx = 0, maxIdx = _count >> 1; idx < maxIdx; idx++)
                {
                    var temp = _items[idx];
                    _items[idx] = _items[_count - idx - 1];
                    _items[_count - idx - 1] = temp;
                }
            }
        }

        public T[] ToArray()
        {
            var target = new T[_count];

            if (_count > 0)
            {
                Array.Copy(_items, target, _count);
            }

            return target;
        }

        private readonly bool _isNullable;

        private T[] _items;
        private int _count, _capacity;

        private readonly EqualityComparer<T> _comparer;

        private const int INIT_CAPACITY = 8;
    }
}
