using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using System.Linq;
using GameEngine.Globals;

namespace GameEngine.Tools
{
    public class JsonFieldValueConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            if (token.Type == JTokenType.String)
            {
                // It's a single string
                return token.ToString();
            }
            else if (token.Type == JTokenType.Array)
            {
                // It's an array, but we need to check what kind of array

                var firstElement = token.First;

                if (firstElement != null && firstElement.Type == JTokenType.String)
                {
                    // Array of strings
                    return token.ToObject<List<string>>();
                }
                else if (firstElement != null && firstElement["cx"] != null && firstElement["cy"] != null)
                {
                    var points = token.ToObject<List<PointData>>();
                    return points.Select(p => new Vector2(p.Cx * GameSettings.TileSize, p.Cy * GameSettings.TileSize)).ToList();
                }
            }

            // Fallback to returning the raw token if type is unknown
            return token.ToString();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public struct PointData
    {
        public float Cx { get; set; }
        public float Cy { get; set; }
    }
}