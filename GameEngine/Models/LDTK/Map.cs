using System.Collections.Generic;

namespace GameEngine.Models.LDTK
{
    public class Map
    {
        public static int TileSize { get; set; } = 16;

        public List<Level> Levels { get; set; }
    }
}
