using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameEngine.Models.LDTK
{
    public class LayerInstance
    {
        [JsonProperty("__identifier")]
        public string Name { get; set; }

        public List<AutoLayerTile> AutoLayerTiles { get; set; }

        [JsonProperty("intGridCsv")]
        public int[] Collisions { get; set; }
    }
}
