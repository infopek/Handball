using System;

namespace Handball
{
    public class StaticRandom
    {
        private static Random _random = new Random();

        public static int Next(int lo, int high) => _random.Next(lo, high);       
        public static int Next(int high) => _random.Next(0, high);
    }
}
