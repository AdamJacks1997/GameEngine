using GameEngine.Models.ECS.Core;
using GameEngine.Models.LDTK;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Globals
{
    public static class Globals
    {
        public static ContentManager ContentManager { get; set; }

        public static SpriteBatch SpriteBatch { get; set; }

        public static Level CurrentLevel { get; set; }

        public static int[][] CurrentCollisions { get; set; }

        public static Matrix CameraMatrix { get; set; }

        public static Vector2 CameraFocusPosition { get; set; }

        public static Vector2 CameraPosition { get; set; }

        public static Entity PlayerEntity { get; set; }
    }
}
