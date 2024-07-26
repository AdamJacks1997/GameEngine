using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class EquippedEntityComponent : IComponent
    {
        public Entity Entity;

        public Point Offset;
    }
}
