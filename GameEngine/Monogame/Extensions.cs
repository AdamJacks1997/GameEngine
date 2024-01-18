using Microsoft.Xna.Framework;

namespace GameEngine.Monogame
{
    public static class Extensions
    {
        public static Vector2 NormalizeWithZeroCheck(this Vector2 vector)
        {
            if (vector == Vector2.Zero)
            {
                return vector;
            }

            vector.Normalize();

            return vector;
        }
    }
}
