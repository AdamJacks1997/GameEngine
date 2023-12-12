using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameEngine.Models.LDTK
{
    public class LayerInstance
    {
        [JsonProperty("__identifier")]
        public string Name { get; set; }

        public List<AutoLayerTile> AutoLayerTiles { get; set; }

        //[JsonProperty("intGridCsv")]
        //public int[] Collisions { get; set; }

        //public bool HasCollisionAt(Vector2 position)
        //{
        //    return Collisions[(position.Y * GridSize.X) + position.X]; // TEST THIS - GridSize wrong?
        //}
    }
}
