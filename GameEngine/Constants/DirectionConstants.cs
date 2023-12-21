using GameEngine.Enums;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameEngine.Constants
{
    public class DirectionConstants
    {
        public static Dictionary<Vector2, DirectionEnum> DirectionDictionary = new Dictionary<Vector2, DirectionEnum>();

        public DirectionConstants()
        {
            DirectionDictionary = new Dictionary<Vector2, DirectionEnum>()
            { 
                { new Vector2(1,0), DirectionEnum.Right },
                { new Vector2(-1,0), DirectionEnum.Left },

                { new Vector2(0,-1), DirectionEnum.Up },
                { new Vector2(1,-1), DirectionEnum.Up },
                { new Vector2(-1,-1), DirectionEnum.Up },

                { new Vector2(0,1), DirectionEnum.Down },
                { new Vector2(1,1), DirectionEnum.Down },
                { new Vector2(-1,1), DirectionEnum.Down },
            };
        }

        public static readonly List<Point> Directions = new List<Point>
        {
            new Point(0, -1),    //  Up
            new Point(0, 1),     //  Down
            new Point(-1, 0),    //  Left
            new Point(1, 0),     //  Right
            
            //new Point(-1, -1),    //  Up Left
            //new Point(1, -1),    //  Up Right
            //new Point(-1, 1),    //  Down Left
            //new Point(1, 1),    //  Down Right
        };
    }
}
