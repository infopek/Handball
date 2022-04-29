using NUnit.Framework;
using Handball.Collections.Generic;

namespace Handball_Tests.LinkedList
{
    [TestFixture]
    public class DeleteTests
    {
        [TestCase]
        public void Delete_Deletes_Item()
        {
            LinkedList<double> list = new();
            LinkedList<double> expected = new();

            expected.Insert(4.5d);
            expected.Insert(6.7d);
            expected.Insert(2.67d);
            expected.Insert(0.57d);

            list.Insert(4.5d);
            list.Insert(6.7d);
            list.Insert(1.34d);
            list.Insert(2.67d);
            list.Insert(0.57d);

            list.Delete(1.34d);

            Assert.AreEqual(expected, list);
        }

        [TestCase]
        public void Delete_ThrowsException()
        {
            LinkedList<double> list = new();

            list.Insert(4.73d);
            list.Insert(918.2d);

            Assert.Throws(typeof(ElementNotFoundException), () => list.Delete(54.6d));
        }
    }
}
