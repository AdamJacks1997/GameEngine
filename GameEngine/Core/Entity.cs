using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Core
{
    public abstract class Entity : Component
    {
        public Vector2 Position;
        public int Width = 100;
        public int Height = 100;
        Vector2 Location { get; set; }
        Vector2 Velocity { get; set; }
        public Texture2D Texture;
    }
}
