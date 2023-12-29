using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Components
{
    public class SpriteComponent : IComponent
    {
        public Texture2D Texture;

        public Point Offset;

        public Rectangle Source;

        public float Layer;

        public float Rotation = 0;
    }
}
