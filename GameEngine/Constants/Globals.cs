using GameEngine.Models.ECS.Core;
using GameEngine.Models.LDTK;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Constants
{
    public class Globals
    {
        public static SpriteBatch SpriteBatch { get; set; }

        public static Level CurrentLevel { get; set; }

        public static int[][] CurrentCollisions { get; set; }

        public static int[][] WeightedCollisions { get; set; }

        public static Vector2 CameraPosition { get; set; }

        public static Entity PlayerEntity { get; set; }
    }
}
