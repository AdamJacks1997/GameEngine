
using GameEngine.Models.LDTK;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using Template.Entities;
using GameEngine.Globals;
using System.Collections.Generic;

namespace Template.Handlers
{
    public class LdtkHandler
    {
        private readonly Map _map = new Map();
        private int[][] _collisions;
        private Level _currentLevel;

        public LdtkHandler()
        {
            var mapDataJson = LoadFile("../../../Map/", "Map.ldtk");

            _map = JsonConvert.DeserializeObject<Map>(mapDataJson);
        }

        public void LoadLevel()
        {
            _currentLevel = _map.Levels.SingleOrDefault(l => l.Name == Globals.CurrentLevelName);
            Globals.CurrentLevel = _currentLevel;

            PopulateFloorTiles();

            PopulateWallTiles();

            PopulateCharacterEntities();

            PopulateSceneEntities();
        }

        private void PopulateFloorTiles()
        {
            var floor = _currentLevel.LayerInstances.Single(li => li.Name == "Floor");

            floor.AutoLayerTiles.ForEach(tile =>
            {
                new TileEntity(tile.Position, tile.Source, 0f);
            });
        }

        private void PopulateWallTiles()
        {
            var walls = _currentLevel.LayerInstances.Single(li => li.Name == "Walls");

            _collisions = new int[(int)_currentLevel.Size.Y / GameSettings.TileSize][];

            for (int y = 0; y < _currentLevel.Size.Y / GameSettings.TileSize; y++)
            {
                _collisions[y] = new int[(int)_currentLevel.Size.X / GameSettings.TileSize];

                for (int x = 0; x < _currentLevel.Size.X / GameSettings.TileSize; x++)
                {
                    _collisions[y][x] = walls.Collisions[y * (int)_currentLevel.Size.X / GameSettings.TileSize + x];
                }
            }

            walls.AutoLayerTiles.ForEach(tile =>
            {
                if (_collisions[(int)tile.Position.Y / GameSettings.TileSize][(int)tile.Position.X / GameSettings.TileSize] == 1)
                {
                    new TileEntity(tile.Position, tile.Source, 0.1f, new Rectangle((int)tile.Position.X, (int)tile.Position.Y, GameSettings.TileSize, GameSettings.TileSize));
                }
                else
                {
                    new TileEntity(tile.Position, tile.Source, 0.1f);
                }
            });
            Globals.CurrentCollisions = _collisions;
        }

        private void PopulateCharacterEntities()
        {
            var entities = _currentLevel.LayerInstances.Single(li => li.Name == "Characters");

            entities.EntityInstances.ForEach(entity =>
            {
                switch (entity.Identifier)
                {
                    case "Player":
                        Globals.PlayerEntity = new PlayerEntity(entity.Position);
                        Globals.CameraEntity = Globals.PlayerEntity;
                        break;
                    case "EnemySpawner":
                        new EnemySpawnerEntity(entity.Position);
                        break;
                }
            });
        }

        private void PopulateSceneEntities()
        {
            var entities = _currentLevel.LayerInstances.Single(li => li.Name == "Scenes");

            entities.EntityInstances.ForEach(entity =>
            {
                switch (entity.Identifier)
                {
                    case "TriggerArea":
                        new SceneTriggerAreaEntity(
                            entity.Position,
                            new Point(entity.Width, entity.Height),
                            entity.FieldInstances.Where(e => e.Identifier == "SceneName").Select(e => e.Value as string).FirstOrDefault());
                        break;
                    case "Camera":
                        new CameraEntity(
                            entity.FieldInstances.Where(e => e.Identifier == "SceneName").Select(e => e.Value as string).FirstOrDefault(),
                            entity.Position);
                        break;
                    case "Character":
                        new SceneCharacterEntity(
                            entity.FieldInstances.Where(e => e.Identifier == "SceneName").Select(e => e.Value as string).FirstOrDefault(),
                            entity.FieldInstances.Where(e => e.Identifier == "CharacterName").Select(e => e.Value as string).FirstOrDefault(),
                            entity.FieldInstances.Where(e => e.Identifier == "Movements").Select(e => e.Value as List<Vector2>).FirstOrDefault(),
                            entity.Position);
                        break;
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
