using GameEngine.Tools;
using Newtonsoft.Json;

namespace GameEngine.Models.LDTK
{
    public class FieldInstance
    {
        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        [JsonProperty("__type")]
        [JsonConverter(typeof(JsonValueConverter))]
        public string Type { get; set; }

        [JsonProperty("__value")]
        public object Value { get; set; }
    }
}
