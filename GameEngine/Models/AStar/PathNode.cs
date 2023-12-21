using Microsoft.Xna.Framework;
using System;

namespace GameEngine.Models.AStar
{
    public class PathNode
    {
        public const int TileCost = 10;

        public PathNode Parent { get; set; }

        public float CostFromStartPosition { get; set; }

        public float CostToGoalPosition { get; set; }

        public float TotalCostOfNode => CostFromStartPosition * CostToGoalPosition;

        public Point GridPosition { get; set; }

        // The first Position here will be the starting position
        public PathNode(Point gridPosition, Point goalTilePosition, PathNode parent)
        {
            GridPosition = gridPosition;
            Parent = parent;

            var distanceX = Math.Abs(gridPosition.X - goalTilePosition.X);
            var distanceY = Math.Abs(gridPosition.Y - goalTilePosition.Y);

            CostToGoalPosition = (distanceX + distanceY) * TileCost;

            CostFromStartPosition = TileCost;

            if (parent != null)
            {
                CostFromStartPosition += parent.CostFromStartPosition;
            }
        }
    }
}
