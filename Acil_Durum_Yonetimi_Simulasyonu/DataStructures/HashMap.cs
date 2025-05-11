using System;
using System.Collections.Generic;
using System.Linq;

namespace Acil_Durum_Yonetimi_Simulasyonu.DataStructures
{
    public class HashMap<TKey, TValue>
    {
        private LinkedList<KeyValuePair<TKey, TValue>>[] _buckets;
        private int _capacity;
        private readonly float _loadFactor;
        private int _size;

        public event Action HashMapChanged;

        public int Count => _size;

        public HashMap(int capacity = 16, float loadFactor = 0.75f)
        {
            _capacity = capacity;
            _loadFactor = loadFactor;
            _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];
            _size = 0;
        }

        private int GetBucketIndex(TKey key)
        {
            return Math.Abs(key.GetHashCode() % _capacity);
        }

        public void Put(TKey key, TValue value)
        {
            if (_size >= _capacity * _loadFactor)
                Resize();

            int bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];

            if (bucket == null)
            {
                bucket = new LinkedList<KeyValuePair<TKey, TValue>>();
                _buckets[bucketIndex] = bucket;
            }

            foreach (var node in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(node.Key, key))
                {
                    bucket.Remove(node);
                    _size--;
                    break;
                }
            }

            bucket.Add(new KeyValuePair<TKey, TValue>(key, value));
            _size++;
            OnHashMapChanged();
        }

        public bool Remove(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];

            if (bucket == null) return false;

            foreach (var node in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(node.Key, key))
                {
                    bucket.Remove(node);
                    _size--;
                    OnHashMapChanged();
                    return true;
                }
            }

            return false;
        }

        public void Clear()
        {
            _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[_capacity];
            _size = 0;
            OnHashMapChanged();
        }

        public TValue Get(TKey key)
        {
            int bucketIndex = GetBucketIndex(key);
            var bucket = _buckets[bucketIndex];

            if (bucket == null)
                throw new KeyNotFoundException($"Key '{key}' not found.");

            foreach (var node in bucket)
            {
                if (EqualityComparer<TKey>.Default.Equals(node.Key, key))
                {
                    return node.Value;
                }
            }

            throw new KeyNotFoundException($"Key '{key}' not found.");
        }

        public IEnumerable<TKey> GetKeys()
        {
            foreach (var bucket in _buckets)
            {
                if (bucket != null)
                {
                    foreach (var pair in bucket)
                    {
                        yield return pair.Key;
                    }
                }
            }
        }

        private void Resize()
        {
            int newCapacity = _capacity * 2;
            var newBuckets = new LinkedList<KeyValuePair<TKey, TValue>>[newCapacity];

            foreach (var bucket in _buckets)
            {
                if (bucket == null) continue;

                foreach (var pair in bucket)
                {
                    int newIndex = Math.Abs(pair.Key.GetHashCode() % newCapacity);

                    if (newBuckets[newIndex] == null)
                        newBuckets[newIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();

                    newBuckets[newIndex].Add(pair); 
                }
            }

            _buckets = newBuckets;
            _capacity = newCapacity;
            OnHashMapChanged();
        }

        private void OnHashMapChanged()
        {
            HashMapChanged?.Invoke();
        }
    }
}
