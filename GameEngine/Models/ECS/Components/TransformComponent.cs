﻿using GameEngine.Globals;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class TransformComponent : IComponent
    {
        public Vector2 Position;

        public Vector2 MidPosition => new Vector2(Position.X + Size.X / 2, Position.Y);

        public Point GridPosition => new Point((int)((Position.X + (Size.X / 2)) / GameSettings.TileSize), (int)((Position.Y + (Size.Y / 2)) / GameSettings.TileSize));

        public Point Size;
    }
}
