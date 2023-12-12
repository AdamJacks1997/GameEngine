using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine.Constants
{
    public static class GameSettings
    {
        public static int ScreenWidth { get; set; } = 1366;
        public static int ScreenHeight { get; set; } = 768;

        public static int TileSize { get; } = 16;

        public static bool Exit { get; set; } = false;
    }
}
