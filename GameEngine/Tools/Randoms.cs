using Microsoft.Xna.Framework;
using System;

namespace GameEngineTools
{
    public class Randoms
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

        public static Point PointWithinRadius(Point point, int radius)
        {
            Random random = new Random();

            point.X += random.Next(-radius, radius + 1);
            point.Y += random.Next(-radius, radius + 1);

            return point;
        }
    }
}
