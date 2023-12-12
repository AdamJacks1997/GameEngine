using GameEngine.Models.ECS;
using Microsoft.Xna.Framework;

namespace Template.Components
{
    public class TransformComponent : IComponent
    {
        public Vector2 Position;

        public Vector2 PreviousPosition;

        public Point Size;

        public Rectangle Bounds => new Rectangle(Position.ToPoint(), Size);
    }
}
