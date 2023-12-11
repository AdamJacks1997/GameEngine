using GameEngine.Models.LDTK;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Handlers
{
    public class LdtkHandler
    {
        public Map currentMap = new Map();
        private TextureHandler _textureHandler;

        public LdtkHandler(TextureHandler textureHandler)
        {
            _textureHandler = textureHandler;
        }

        public Map Init(string level)
        {
            var mapDataJson = LoadFile("D:/Projects/GameEngine/Template/Map/" + level + "/", "data.json");

            currentMap = JsonConvert.DeserializeObject<Map>(mapDataJson);

            return currentMap;
        }

        private string LoadFile(string path, string name)
        {
            string mapFilePath = Path.Combine(path, name);
            var reader = new StreamReader(mapFilePath);

            return reader.ReadToEnd();
        }
    }
}
