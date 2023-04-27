using GameEngine.Core;

namespace GameEngine.Physics
{
    internal class Collides
    {
        public static bool WithScreenHorizontal(float xPosition, int width)
        {
            if (xPosition < 0) // Collides Left
            {
                return true;
            }

            if (xPosition > Data.ScreenWidth - width) // Collides Right
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenVertical(float yPosition, int height)
        {
            if (yPosition < 0) // Collides Top
            {
                return true;
            }

            if (yPosition > Data.ScreenHeight - height) // Collides Bottom
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenLeft(float xPosition, int width)
        {
            if (xPosition < 0)
            {
                return true;
            }

            return false;
        }
        public static bool WithScreenRight(float xPosition, int width)
        {
            if (xPosition > Data.ScreenWidth - width)
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenTop(float yPosition, int height)
        {
            if (yPosition < 0)
            {
                return true;
            }

            return false;
        }

        public static bool WithScreenBottom(float yPosition, int height)
        {
            if (yPosition > Data.ScreenHeight - height)
            {
                return true;
            }

            return false;
        }

        public static bool AABB(Entity one, Entity two)
        {
            return one.Position.X < two.Position.X + two.Width &&
                   one.Position.X + one.Width > two.Position.X &&
                   one.Position.Y < two.Position.Y + two.Height &&
                   one.Position.Y + one.Height > two.Position.Y;
        }
    }
}
