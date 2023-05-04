using System.Collections.Generic;
using System.IO;
using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace GameEngine.Handlers
{
    public class TileHandler
    {
        private static List<Tile> _currentMap;

        public void Init(string map)
        {
            string mapFilePath = Path.Combine("D:/Projects/GameEngine/Template/Maps", $"{map}.json");
            using StreamReader reader = new StreamReader(mapFilePath);
            string mapJson = reader.ReadToEnd();
            _currentMap = JsonConvert.DeserializeObject<List<Tile>>(mapJson);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile tile in _currentMap)
            {
                tile.DrawSprite(spriteBatch);
            }
        }
    }
}