using GameEngine.Constants;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Components
{
    public class ColliderComponent : IComponent
    {
        public int Width;

        public int Height;

        public Point Offset;

        public Rectangle Bounds => new Rectangle((int)Math.Round(ParentEntity.Transform.Position.X + Offset.X), (int)Math.Round(ParentEntity.Transform.Position.Y + Offset.Y), Width, Height);
    }
}
