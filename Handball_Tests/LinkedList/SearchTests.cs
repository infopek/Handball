using NUnit.Framework;
using Handball.Collections.Generic;

namespace Handball_Tests.LinkedList
{
    [TestFixture]
    public class SearchTests
    {
        [TestCase]
        public void Search_Finds_Item()
        {
            LinkedList<string> list = new();
            string expected = "Lekpo";

            list.Insert("Antie");
            list.Insert("Bogie");
            list.Insert("Lekpo");
            list.Insert("Herioff");

            Assert.AreEqual(list.Search(x => x == "Lekpo"), expected);
        }

        [TestCase]
        public void Search_ThrowsException()
        {
            LinkedList<string> list = new();

            list.Insert("Braf");
            list.Insert("Vau");

            Assert.Throws(typeof(ElementNotFoundException), () => list.Search(x => x == "Li"));
        }
    }
}
