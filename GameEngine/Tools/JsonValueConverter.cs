using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine.Tools
{
    public class JsonValueConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            // Check the type of the token to determine how to deserialize
            if (token.Type == JTokenType.String)
            {
                return token.ToObject<string>();
            }
            else if (token.Type == JTokenType.Array)
            {
                return token.ToObject<List<Vector2>>();
            }

            // You can handle other types as needed

            throw new JsonSerializationException($"Unexpected token type: {token.Type}");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Implement this if you need to serialize
            throw new NotImplementedException();
        }
    }
}
