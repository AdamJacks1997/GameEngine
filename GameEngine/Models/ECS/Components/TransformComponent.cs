using GameEngine.Constants;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class TransformComponent : IComponent
    {
        public Vector2 Position;

        public Vector2 GridPosition => new Vector2(Position.X / GameSettings.TileSize, Position.Y / GameSettings.TileSize);

        public Point Size;
    }
}
