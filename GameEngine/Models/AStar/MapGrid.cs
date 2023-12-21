//using Microsoft.Xna.Framework;
//using System.Collections.Generic;

//namespace GameEngine.Models.AStar
//{
//    public class MapGrid
//    {
//        public static readonly Point[] Directions = new[]
//        {
//            new Point(0, 1),     //  Up
//            new Point(0, -1),    //  Down
//            new Point(-1, 0),    //  Left
//            new Point(1, 0),     //  Right
//        };

//        private MapGridTile? _top, _right, _bottom, _left;

//        public Vector2 Size { get; set; }

//        private int[][] _collisions;

//        public void Init(Vector2 size, int[][] collisions)
//        {
//            Size = size;
//            _collisions = collisions;
//        }

//        public bool InGridBounds(MapGridTile tile)
//        {
//            return 0 <= tile.Position.X && tile.Position.X < Size.X &&
//                   0 <= tile.Position.Y && tile.Position.Y < Size.Y;
//        }

//        public bool Passable(MapGridTile tile)
//        {
//            return _collisions[tile.Position.X][tile.Position.Y] != 1;
//        }

//        public IEnumerable<MapGridTile> PassableNeighbors(MapGridTile tile)
//        { // this should be done in the handler
//            foreach (Point direction in Directions)
//            {

//                // only create tile which isn't touched nor a wall
//                // create tile which is closest
//                // manhatten distance?
//                var nextTile = new MapGridTile(new Point(tile.Position.X + direction.Position.X, tile.Position.Y + direction.Position.Y));

//                if (InGridBounds(nextTile) && Passable(nextTile))
//                {
//                    yield return nextTile;
//                }
//            }
//        }
//    }
//}
