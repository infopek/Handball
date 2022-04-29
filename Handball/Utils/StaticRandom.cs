using System;
using System.Collections.Generic;
using System.Linq;
using Handball.Player;

namespace Handball.Utils
{
    public class StaticRandom
    {
        private static readonly Random _random = new();
      
        public static int Next(int hi) => _random.Next(0, hi);
        /// <summary>
        /// Simulates probability accurately enough.
        /// </summary>
        public static bool Chance(float prob) => ((float)Next(int.MaxValue) / int.MaxValue) <= prob;
        /// <param name="n">Number of integers.</param>
        /// <returns>An array containing the <paramref name="n"/> unique numbers.</returns>
        public static int[] GetNUnique(int n, int lo, int hi)
        {
            HashSet<int> uniqueValues = new();
            while (uniqueValues.Count != n)
            {
                uniqueValues.Add(_random.Next(lo, hi));
            }

            return uniqueValues.ToArray();
        }
        public static IPlayer Choice(IPlayer[] playersA, IPlayer[] playersB)
        {
            IPlayer[] chosen = Chance(0.5f) ? playersA : playersB;

            int len = chosen.Length;
            int index = 0;
            bool found = false;
            while (!found)
            {
                if (Chance(1.0f / len)) found = true;
                else                    index = (index + 1) % len;
            }

            return chosen[index];
        }
    }
}
