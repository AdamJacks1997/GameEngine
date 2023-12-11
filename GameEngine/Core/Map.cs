using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    public class Map
    {
        public Texture2D Texture { get; set; }

        public MapData Data { get; set; }

        public int[][] Collisions { get; set; }
    }
}
