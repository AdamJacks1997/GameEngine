using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameEngine.Models.LDTK
{
    public class EntityInstance
    {
        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        [JsonProperty("px")]
        private int[] _positionArray { get; set; }

        public Vector2 Position => new Vector2(_positionArray[0], _positionArray[1]);

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("fieldInstances")]
        public List<FieldInstance> FieldInstances { get; set; }
    }
}
