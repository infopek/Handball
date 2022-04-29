using System;
using System.Collections;
using System.Collections.Generic;

namespace Handball.Collections.Generic
{
    public delegate void TraverseHandler<T>(T data);
    public class LinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> _head;

        public LinkedList()
        {
            this._head = null;
        }

        public T Get_Head() => _head.Data;
        public void Insert(T data)
        {
            Node<T> newNode = new Node<T>();
            newNode.Data = data;

            Node<T> curr = _head;
            Node<T> prev = null;

            // Iterate through the list until a "greater" element is found
            curr = _head;
            while (curr != null && curr.Data.CompareTo(data) < 0)
            {
                prev = curr;
                curr = curr.Next;
            }
            if (prev == null)
            {
                // The list is empty
                newNode.Next = _head;
                _head = newNode;
            }
            else
            {
                newNode.Next = curr;
                prev.Next = newNode;
            }
        }
        public T Search(Predicate<T> conditions)
        {
            Node<T> curr = _head;
            while (curr != null)
            {
                if (conditions(curr.Data))               
                    return curr.Data;               
                curr = curr.Next;
            }

            throw new ElementNotFoundException();
        }
        public void Delete(T item)
        {
            Node<T> curr = _head;
            Node<T> prev = null;

            // Deletes the first element that satisfies the conditions
            while (curr != null && !curr.Data.Equals(item))
            {
                prev = curr;
                curr = curr.Next;
            }
            if (curr != null)
            {
                // The list contains such element
                if (prev == null)
                {
                    // The element to delete is the _head
                    _head = curr.Next;
                }
                else
                {
                    prev.Next = curr.Next;
                }
            }
            else
            {
                throw new ElementNotFoundException();
            }
        }
        public void Traverse(TraverseHandler<T> handler)
        {
            TraverseHandler<T> _handler = handler;
            Node<T> curr = _head;
            while (curr != null)
            {
                _handler?.Invoke(curr.Data);
                curr = curr.Next;
            }
        }

        public IEnumerator<T> GetEnumerator() => new Traverser<T>(_head);
        IEnumerator IEnumerable.GetEnumerator() => new Traverser<T>(_head);
    }
}
