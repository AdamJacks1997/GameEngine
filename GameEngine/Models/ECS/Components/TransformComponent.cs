using GameEngine.Globals;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class TransformComponent : IComponent
    {
        public Vector2 Position;

        // Unsure if centering the position before dividing by tilesize helps or not
        //public Point GridPosition => new Point((int)Math.Round((Position.X - GameSettings.TileSize / 2) / GameSettings.TileSize), (int)Math.Round((Position.Y - GameSettings.TileSize / 2) / GameSettings.TileSize));
        //public Point GridPosition => new Point((int)Math.Round(Position.X / GameSettings.TileSize), (int)Math.Round(Position.Y / GameSettings.TileSize));
        //public Point GridPosition => new Point((int)(Position.X / GameSettings.TileSize), (int)(Position.Y / GameSettings.TileSize));
        public Point GridPosition => new Point((int)((Position.X + (GameSettings.TileSize / 2) + 1) / GameSettings.TileSize), (int)((Position.Y + (GameSettings.TileSize / 2) + 1) / GameSettings.TileSize));

        public Point Size;
    }
}
