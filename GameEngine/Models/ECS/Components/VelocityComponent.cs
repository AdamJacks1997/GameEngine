using GameEngine.Enums;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class VelocityComponent : IComponent
    {
        public Vector2 DirectionVector = Vector2.Zero;

        public DirectionEnum Direction
        {
            get
            {
                switch(DirectionVector)
                {
                    case Vector2(1, 0):
                        return DirectionEnum.Right;
                    case Vector2(-1, 0):
                        return DirectionEnum.Left;
                    case Vector2(0, -1):
                        return DirectionEnum.Up;
                    case Vector2(1, -1):
                        return DirectionEnum.Up;
                    case Vector2(-1, -1):
                        return DirectionEnum.Up;
                    case Vector2(0, 1):
                        return DirectionEnum.Down;
                    case Vector2(1, 1):
                        return DirectionEnum.Down;
                    case Vector2(-1, 1):
                        return DirectionEnum.Down;
                    default: return DirectionEnum.Down;
                }
            }
        }

        public DirectionEnum LastDirection = DirectionEnum.Down;

        public float Speed = 0f;
    }
}
