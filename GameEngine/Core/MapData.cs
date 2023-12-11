using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    public class MapData
    {
        public static int TileSize { get; set; } = 16;

        [JsonProperty("width")]
        public int Width {
            get
            {
                return _width;
            }

            set
            {
                _width = value / TileSize;
            }
        }

        [JsonProperty("height")]
        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value / TileSize;
            }
        }

        private int _width;

        private int _height;
    }
}
