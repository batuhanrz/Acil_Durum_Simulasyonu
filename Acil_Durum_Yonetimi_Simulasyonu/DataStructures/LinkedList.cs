using System;
using System.Collections;
using System.Collections.Generic;

namespace Acil_Durum_Yonetimi_Simulasyonu.DataStructures
{
    public class LinkedListNode<T>
    {
        public T Data { get; set; }
        public LinkedListNode<T> Next { get; set; }

        public LinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedList<T> : IEnumerable<T>
    {
        private LinkedListNode<T> _head;
        private LinkedListNode<T> _tail;
        public int Count { get; private set; }

        public event Action ListChanged;

        public LinkedList()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public void Add(T data)
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(data);

            if (_head == null)
            {
                _head = newNode;
                _tail = newNode;
            }
            else
            {
                _tail.Next = newNode;
                _tail = newNode;
            }

            Count++;
            OnListChanged();
        }

        public bool Remove(T data)
        {
            if (_head == null) return false;

            LinkedListNode<T> current = _head;
            LinkedListNode<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous == null) _head = current.Next;
                    else previous.Next = current.Next;

                    if (current == _tail) _tail = previous;

                    Count--;
                    OnListChanged();
                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
            OnListChanged();
        }
        private void OnListChanged()
        {
            ListChanged?.Invoke();
        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
