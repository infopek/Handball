using System;
using System.Collections.Generic;

namespace Handball.Collections.Generic
{
    public class Traverser<T> : IEnumerator<T>
        where T : IComparable<T>
    {
        private Node<T> head;
        private Node<T> curr;

        public Traverser(Node<T> head)
        {
            this.head = head;
            curr = new Node<T>();
            curr.Next = head;
        }

        public object Current { get => curr.Data; }
        T IEnumerator<T>.Current { get => curr.Data; }

        public bool MoveNext()
        {
            if (curr == null)           
                return false;        
            curr = curr.Next;
            return curr != null;
        }
        public void Reset()
        {
            curr = new Node<T>();
            curr.Next = head;
        }
        public void Dispose() { }
    }
}
