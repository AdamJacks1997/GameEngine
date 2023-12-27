using GameEngine.Globals;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class TransformComponent : IComponent
    {
        public Vector2 Position;

        public Point GridPosition => new Point((int)((Position.X + (GameSettings.TileSize / 2)) / GameSettings.TileSize), (int)((Position.Y + (GameSettings.TileSize / 2)) / GameSettings.TileSize));

        public Vector3 Position3D => new Vector3(Position.X, Position.Y, 0);

        public Point Size;
    }
}
