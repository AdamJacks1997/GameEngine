using System;

namespace GameEngineTools
{
    internal class Randoms
    {
        public static int IntBetween(int min, int max)
        {
            Random random = new Random();

            return random.Next(min, max + 1);
        }

        public static bool Bool()
        {
            Random random = new Random();

            return random.Next() > (Int32.MaxValue / 2);
        }
    }
}
