using GameEngine.Constants;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;

namespace GameEngine.Components
{
    public class ColliderComponent : IComponent
    {
        public Rectangle Bounds;

        //public Point GridPosition => new Point((int)(Bounds.X / GameSettings.TileSize), (int)(Bounds.Y / GameSettings.TileSize));

        public Point Offset;
    }
}
