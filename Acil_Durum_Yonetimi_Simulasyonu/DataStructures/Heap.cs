using System;
using System.Collections;
using System.Collections.Generic;

namespace Acil_Durum_Yonetimi_Simulasyonu.DataStructures
{
    public class Heap<T> : IEnumerable<T>
    {
        private readonly List<T> _items;
        private readonly IComparer<T> _comparer;

        public int Count => _items.Count;

        public event Action HeapChanged;

        public Heap(IComparer<T> comparer = null)
        {
            _items = new List<T>();
            _comparer = comparer ?? Comparer<T>.Default;
        }

        public void Enqueue(T item)
        {
            _items.Add(item);
            HeapifyUp(_items.Count - 1);
            OnHeapChanged();
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            T root = _items[0];
            _items[0] = _items[_items.Count - 1];
            _items.RemoveAt(_items.Count - 1);

            if (_items.Count > 0)
                HeapifyDown(0);

            OnHeapChanged();
            return root;
        }

        public T Peek()
        {
            if (_items.Count == 0)
                throw new InvalidOperationException("Heap is empty.");

            return _items[0];
        }
        public void Heapify()
        {
            int lastNonLeafIndex = (_items.Count / 2) - 1;

            for (int i = lastNonLeafIndex; i >= 0; i--)
            {
                HeapifyDown(i);
            }

            OnHeapChanged();
        }

        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;

                if (_comparer.Compare(_items[index], _items[parentIndex]) >= 0)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        private void HeapifyDown(int index)
        {
            int lastIndex = _items.Count - 1;

            while (index < lastIndex)
            {
                int leftChildIndex = 2 * index + 1;
                int rightChildIndex = 2 * index + 2;
                int smallestIndex = index;

                if (leftChildIndex <= lastIndex && _comparer.Compare(_items[leftChildIndex], _items[smallestIndex]) < 0)
                    smallestIndex = leftChildIndex;

                if (rightChildIndex <= lastIndex && _comparer.Compare(_items[rightChildIndex], _items[smallestIndex]) < 0)
                    smallestIndex = rightChildIndex;

                if (smallestIndex == index)
                    break;

                Swap(index, smallestIndex);
                index = smallestIndex;
            }
        }

        public void Clear()
        {
            _items.Clear();
            OnHeapChanged();
        }

        private void Swap(int indexA, int indexB)
        {
            T temp = _items[indexA];
            _items[indexA] = _items[indexB];
            _items[indexB] = temp;
        }

        private void OnHeapChanged()
        {
            HeapChanged?.Invoke();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _items)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
