using GameEngine.Constants;
using GameEngine.Handlers;
using GameEngine.Models.LDTK;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Template.Entities;

namespace Template.Handlers
{
    public class LdtkHandler
    {
        private readonly TextureHandler _textureHandler;
        private readonly Map _map = new Map();

        public LdtkHandler(TextureHandler textureHandler)
        {
            _textureHandler = textureHandler;

            var mapDataJson = LoadFile("D:/Projects/GameEngine/Template/Map/", "Map.ldtk");

            _map = JsonConvert.DeserializeObject<Map>(mapDataJson);
        }

        public void LoadLevel(int level)
        {
            Level currentLevel = _map.Levels[level];
            Globals.CurrentLevel = currentLevel;

            var floor = currentLevel.LayerInstances.Single(li => li.Name == "Floor");

            var walls = currentLevel.LayerInstances.Single(li => li.Name == "Walls");

            floor.AutoLayerTiles.ForEach(tile =>
            {
                new Tile(_textureHandler, tile.PixelPosition, tile.Source, 0f);
            });

            walls.AutoLayerTiles.ForEach(tile =>
            {
                new Tile(_textureHandler, tile.PixelPosition, tile.Source, 0.1f);
            });
        }

        private string LoadFile(string path, string name)
        {
            string mapFilePath = Path.Combine(path, name);
            var reader = new StreamReader(mapFilePath);

            return reader.ReadToEnd();
        }
    }
}
