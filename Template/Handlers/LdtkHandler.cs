using GameEngine.Constants;
using GameEngine.Handlers;
using GameEngine.Models.LDTK;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Template.Entities;
using GameEngine.Globals;

namespace Template.Handlers
{
    public class LdtkHandler
    {
        private readonly TextureHandler _textureHandler;
        private readonly Map _map = new Map();
        private int[][] _collisions;
        private int[][] _weightedCollisions;

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
                new TileEntity(_textureHandler, tile.Position, tile.Source, 0f);
            });

            _collisions = new int[(int)currentLevel.Size.Y / GameSettings.TileSize][];
            _weightedCollisions = new int[(int)currentLevel.Size.Y / GameSettings.TileSize][];

            for (int y = 0; y < currentLevel.Size.Y / GameSettings.TileSize; y++)
            {
                _collisions[y] = new int[(int)currentLevel.Size.X / GameSettings.TileSize];
                _weightedCollisions[y] = new int[(int)currentLevel.Size.X / GameSettings.TileSize];

                for (int x = 0; x < currentLevel.Size.X / GameSettings.TileSize; x++)
                {
                    _collisions[y][x] = walls.Collisions[y * (int)currentLevel.Size.X / GameSettings.TileSize + x];
                    _weightedCollisions[y][x] = walls.Collisions[y * (int)currentLevel.Size.X / GameSettings.TileSize + x];
                }
            }

            for (int y = 0; y < currentLevel.Size.Y / GameSettings.TileSize; y++)
            {
                for (int x = 0; x < currentLevel.Size.X / GameSettings.TileSize; x++)
                {
                    if (_collisions[y][x] != 1)
                    {
                        continue;
                    }

                    if (y + 1 < 63)
                    {
                        _weightedCollisions[y + 1][x] = 1;
                    }


                    if (y - 1 < 63)
                    {
                        _weightedCollisions[y - 1][x] = 1;
                    }


                    if (x + 1 < 63)
                    {
                        _weightedCollisions[y][x + 1] = 1;
                    }

                    if (x - 1 < 63)
                    {
                        _weightedCollisions[y][x - 1] = 1;
                    }
                }
            }


            Globals.CurrentCollisions = _collisions;
            Globals.WeightedCollisions = _weightedCollisions;

            walls.AutoLayerTiles.ForEach(tile =>
            {
                if (_collisions[(int)tile.Position.Y / GameSettings.TileSize][(int)tile.Position.X / GameSettings.TileSize] == 1)
                {
                    new TileEntity(_textureHandler, tile.Position, tile.Source, 0.1f, new Rectangle((int)tile.Position.X, (int)tile.Position.Y, GameSettings.TileSize, GameSettings.TileSize));
                }
                else
                {
                    new TileEntity(_textureHandler, tile.Position, tile.Source, 0.1f);
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
