using NUnit.Framework;
using Handball.Collections.Generic;

namespace Handball_Tests.LinkedList
{
    [TestFixture]
    public class InsertTests
    {
        [TestCase]
        public void Insert_SingleItem_Becomes_Head()
        {
            LinkedList<int> list = new();

            list.Insert(1);

            Assert.AreEqual(list.GetHead(), 1);
        }

        [TestCase]
        public void Insert_Items_Inserts_In_Order()
        {
            LinkedList<int> list = new();
            int[] expected = { 1, 2, 3, 3, 4, 5, 6, 7, 14 };
            int[] actual = new int[9];

            list.Insert(4);
            list.Insert(2);
            list.Insert(1);
            list.Insert(6);
            list.Insert(3);
            list.Insert(3);
            list.Insert(7);
            list.Insert(14);
            list.Insert(5);

            int index = 0;
            foreach (var item in list)
            {
                actual[index] = item;
                index++;
            }

            Assert.AreEqual(expected, actual);
        }
    }
}