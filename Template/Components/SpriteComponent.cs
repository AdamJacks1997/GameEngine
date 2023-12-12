using GameEngine.Models.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Template.Components
{
    public class SpriteComponent : IComponent
    {
        public Texture2D Texture;

        public Rectangle Source;
    }
}
