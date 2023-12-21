using GameEngine.Constants;
using GameEngine.Models.AStar;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Template.Handlers
{
    public class PathHandler
    {
        private readonly List<PathNode> openList;
        private readonly List<PathNode> closedList;
        private const int MaxDepth = 500;

        public PathHandler()
        {
            openList = new List<PathNode>();
            closedList = new List<PathNode>();
        }

        public List<Point> FindPath(Point startTilePosition, Point goalTilePosition)
        {
            if (startTilePosition == goalTilePosition)
            {
                return new List<Point>();
            }

            openList.Clear();
            closedList.Clear();

            openList.Add(new PathNode(startTilePosition, goalTilePosition, null));

            CheckForPath(openList.First(), goalTilePosition, 0);

            var node = closedList.Last();
            var path = new List<Point>();

            while (node.Parent != null)
            {
                path.Add(node.GridPosition);
                node = node.Parent;
            }

            path.Reverse();

            return path;
        }

        private void CheckForPath(PathNode currentNode, Point goalTilePosition,
            int depth)
        {
            if (depth == MaxDepth)
            {
                return;
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode.GridPosition == goalTilePosition)
            {
                return;
            }

            DirectionConstants.Directions.ForEach(direction =>
            {
                CheckNode(direction, currentNode, goalTilePosition);
            });

            openList.Sort((a, b) =>
            {
                return a.TotalCostOfNode == b.TotalCostOfNode
                ? a.CostToGoalPosition.CompareTo(b.CostToGoalPosition)
                : a.TotalCostOfNode.CompareTo(b.TotalCostOfNode);
            });

            if (openList.Any())
            {
                CheckForPath(openList.First(), goalTilePosition, depth++);
            }
        }

        private void CheckNode(Point direction, PathNode currentNode, Point goalTilePosition)
        {
            var newPosition = currentNode.GridPosition + direction;

            if (Globals.CurrentCollisions[newPosition.Y][newPosition.X] != 1)
            {
                var oldNode = openList.FirstOrDefault(open => open.GridPosition == newPosition);

                if (oldNode != null)
                {
                    if (currentNode.CostFromStartPosition + PathNode.TileCost < oldNode.CostFromStartPosition)
                    {
                        oldNode.Parent = currentNode;
                        oldNode.CostFromStartPosition = currentNode.CostFromStartPosition + PathNode.TileCost;
                    }
                }
                else
                {
                    openList.Add(new PathNode(newPosition, goalTilePosition, currentNode));
                }
            }
        }
    }
}
