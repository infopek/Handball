using System;

namespace Handball
{
    public class StaticRandom
    {
        private static Random _random = new Random();

        public int Next(int lo, int high) => _random.Next(lo, high);       
    }
}
