using GameEngine.Constants;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace GameEngine.Models.LDTK
{
    public class AutoLayerTile
    {
        [JsonProperty("px")]
        private int[] _px { get; set; }

        public Vector2 PixelPosition => new Vector2(
            _px[0],
            _px[1]);

        public Vector2 Position => new Vector2(
            _px[0] / GameSettings.TileSize,
            _px[1] / GameSettings.TileSize);

        [JsonProperty("src")]
        private int[] _src { get; set; }

        public Rectangle Source => new Rectangle(
            _src[0], _src[1], GameSettings.TileSize, GameSettings.TileSize);
    }
}
