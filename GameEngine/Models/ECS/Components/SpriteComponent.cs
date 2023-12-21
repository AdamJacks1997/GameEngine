using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class SpriteComponent : IComponent
    {
        public Texture2D Texture;

        public Vector2 Offset;

        public Rectangle Source;

        public float Layer;
    }
}
