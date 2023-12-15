using GameEngine.Enums;
using GameEngine.Models.ECS;
using Microsoft.Xna.Framework;

namespace Template.Components
{
    public class AttackBoxComponent : IComponent
    {
        public Rectangle Bounds;

        public CollidableTypeEnum CollidableType;
    }
}
