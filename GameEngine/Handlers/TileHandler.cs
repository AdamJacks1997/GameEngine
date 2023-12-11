//using System.Collections.Generic;
//using System.IO;
//using GameEngine.Core;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Newtonsoft.Json;

//namespace GameEngine.Handlers
//{
//    public static class TileHandler
//    {
//        private static HashSet<Tile> _currentMap;
//        public static TextureHandler TextureHandler;

//        public static void Init(string map)
//        {
//            string mapFilePath = Path.Combine("D:/Projects/GameEngine/Template/Maps", $"{map}.json");
//            using StreamReader reader = new StreamReader(mapFilePath);
//            string mapJson = reader.ReadToEnd();

//            _currentMap = JsonConvert.DeserializeObject<HashSet<Tile>>(mapJson);
//        }

//        public static void Draw(SpriteBatch spriteBatch)
//        {
//            foreach (Tile tile in _currentMap)
//            {
//                tile.DrawSprite(spriteBatch);
//            }
//        }

//        public static HashSet<Tile> GetCurrentMap()
//        {
//            return _currentMap;
//        }

//        public static void SetCurrentMap(HashSet<Tile> currentMap)
//        {
//            _currentMap = currentMap;
//        }

//        public static Vector2 LocationToTilePosition(Vector2 location)
//        {
//            return location / Constants.TileSize;
//        }
//    }
//}