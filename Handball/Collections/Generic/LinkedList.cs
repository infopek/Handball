using System;
using System.Collections;
using System.Collections.Generic;

namespace Handball.Collections.Generic
{
    public delegate void TraverseHandler<T>(T data);
    public class LinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> head;

        public LinkedList()
        {
            this.head = null;
        }

        public void Insert(T data)
        {
            Node<T> newNode = new Node<T>();
            newNode.Data = data;

            Node<T> curr = head;
            Node<T> prev = null;

            // Search for possible duplicate
            while (curr != null)
            {
                if (data.Equals(curr.Data))                
                    throw new DuplicateException();               
                curr = curr.Next;
            }

            // Iterate through the list until a "greater" element is found
            curr = head;
            while (curr != null && curr.Data.CompareTo(data) < 0)
            {
                prev = curr;
                curr = curr.Next;
            }
            if (prev == null)
            {
                // The list is empty
                newNode.Next = head;
                head = newNode;
            }
            else
            {
                newNode.Next = curr;
                prev.Next = newNode;
            }
        }
        public T Search(Predicate<T> conditions)
        {
            Node<T> curr = head;
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
            Node<T> curr = head;
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
                    // The element to delete is the head
                    head = curr.Next;
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
            Node<T> curr = head;
            while (curr != null)
            {
                _handler?.Invoke(curr.Data);
                curr = curr.Next;
            }
        }

        public IEnumerator<T> GetEnumerator() => new Traverser<T>(head);
        IEnumerator IEnumerable.GetEnumerator() => new Traverser<T>(head);
    }
}
