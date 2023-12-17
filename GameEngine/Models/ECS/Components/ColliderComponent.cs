using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class ColliderComponent : IComponent
    {
        public Rectangle Bounds;

        public Point Offset;
    }
}
