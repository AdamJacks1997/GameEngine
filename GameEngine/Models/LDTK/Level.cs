using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameEngine.Models.LDTK
{
    public class Level
    {
        [JsonProperty("pxWid")]
        private int _width { get; set; }

        [JsonProperty("pxHei")]
        private int _height { get; set; }

        public Vector2 Size => new Vector2(_width, _height);

        public List<LayerInstance> LayerInstances { get; set; }
    }
}
