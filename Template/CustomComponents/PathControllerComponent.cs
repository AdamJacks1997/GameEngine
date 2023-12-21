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

        public Entity DestinationEntity;

        public int StopDistanceFromTarget = 1;

        public int MaxDistanceFromTarget = 25;

        public int PathRefreshInterval = 60;

        public int PathRefreshCounter = 0;
    }
}
