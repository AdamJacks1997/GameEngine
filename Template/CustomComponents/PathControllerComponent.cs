using GameEngine.Globals;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Template.Handlers;

namespace Template.Components
{
    public class PathControllerComponent : IComponent
    {
        public PathHandler PathHandler;

        public List<Point> CurrentPath;

        public Vector2 Destination;

        public Point GridDestination => new Point((int)((Destination.X + (GameSettings.TileSize / 2)) / GameSettings.TileSize), (int)((Destination.Y + (GameSettings.TileSize / 2)) / GameSettings.TileSize));

        public int PathRefreshInterval = 60;

        public int PathRefreshCounter = 0;
    }
}
