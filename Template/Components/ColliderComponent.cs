using GameEngine.Core;
using GameEngine.Enums;
using Microsoft.Xna.Framework;

namespace Template.Components
{
    public class ColliderComponent : IComponent
    {
        public Rectangle Bounds;

        public CollidableTypeEnum CollidableType;
    }
}
