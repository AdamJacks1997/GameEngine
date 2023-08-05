using GameEngine.Core;
using Microsoft.Xna.Framework;

namespace Template.Components
{
    public class TransformComponent : IComponent
    {
        public Rectangle Bounds => new Rectangle(Position.ToPoint(), Size);
        public Vector2 Position;
        public Vector2 PreviousPosition;
        public Point Size;
    }
}
