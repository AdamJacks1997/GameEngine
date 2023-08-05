using GameEngine.Core;
using GameEngine.Enums;
using Microsoft.Xna.Framework;

namespace Template.Components
{
    public class VelocityComponent : IComponent
    {
        public Vector2 Direction = Vector2.Zero;

        public Vector2 LastDirection = new Vector2(0, 1);

        public float Speed = 0f;
    }
}
