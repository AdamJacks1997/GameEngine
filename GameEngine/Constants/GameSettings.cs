using Microsoft.Xna.Framework;

namespace GameEngine.Constants
{
    public static class GameSettings
    {
        public static Vector2 ScreenSize { get; set; } = new Vector2(1280, 720);
        public static Vector2 NativeSize { get; set; } = new Vector2(640, 360);

        public static int TileSize { get; } = 16;

        public static float DiagnalSpeedMultiplier { get; } = 1.28f;

        public static bool Exit { get; set; } = false;
    }
}
