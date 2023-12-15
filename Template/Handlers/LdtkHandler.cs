using GameEngine.Constants;
using GameEngine.Handlers;
using GameEngine.Models.LDTK;
using Microsoft.Xna.Framework;
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
        private int[][] _collisions;

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
                new TileEntity(_textureHandler, tile.PixelPosition, tile.Source, 0f);
            });

            _collisions = new int[(int)currentLevel.Size.Y / GameSettings.TileSize][];

            for (int y = 0; y < currentLevel.Size.Y / GameSettings.TileSize; y++)
            {
                _collisions[y] = new int[(int)currentLevel.Size.X / GameSettings.TileSize];

                for (int x = 0; x < currentLevel.Size.X / GameSettings.TileSize; x++)
                {
                    _collisions[y][x] = walls.Collisions[y * (int)currentLevel.Size.X / GameSettings.TileSize + x];
                }
            }

            walls.AutoLayerTiles.ForEach(tile =>
            {
                //if (walls.) // if wall at position has a 1 in the intGridCsv then include boundry
                if (_collisions[(int)tile.PixelPosition.Y / GameSettings.TileSize][(int)tile.PixelPosition.X / GameSettings.TileSize] == 1)
                {
                    new TileEntity(_textureHandler, tile.PixelPosition, tile.Source, 0.1f, new Rectangle((int)tile.PixelPosition.X, (int)tile.PixelPosition.Y, GameSettings.TileSize, GameSettings.TileSize));
                }
                else
                {
                    new TileEntity(_textureHandler, tile.PixelPosition, tile.Source, 0.1f);
                }
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
