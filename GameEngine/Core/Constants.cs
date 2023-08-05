﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public static class Constants
    {
        public static int ScreenWidth { get; set; } = 1366;
        public static int ScreenHeight { get; set; } = 768;

        public static int TileSize { get; } = 32;

        public static bool Exit { get; set; } = false;
    }
}
