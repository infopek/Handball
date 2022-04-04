using System;

namespace Handball.Collections.Generic
{
    public class DuplicateException : Exception
    {
        public DuplicateException()
            : base("The given item is already in the list.") { }
    }
}