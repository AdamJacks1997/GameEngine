using GameEngine.Models.LDTK;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Constants
{
    public class Globals
    {
        public static SpriteBatch SpriteBatch { get; set; }

        public static Level CurrentLevel { get; set; }

        public static Vector2 CameraPosition { get; set; }
    }
}
