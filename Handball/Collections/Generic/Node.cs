using System;

namespace Handball.Collections.Generic
{
    public class Node<T>
        where T : IComparable<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
    }
}
