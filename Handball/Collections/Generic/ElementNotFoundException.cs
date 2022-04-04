using System;

namespace Handball.Collections.Generic
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException()
            : base("The element could not be found.") { }
    }
}