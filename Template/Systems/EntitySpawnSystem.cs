using GameEngine.Components;
using GameEngine.Globals;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Template.Components;
using Template.Entities;

namespace Template.Systems
{
    public class EntitySpawnSystem : IUpdateSystem
    {
        private List<Entity> _enemySpawners;

        private Entity _player;

        public void Update(GameTime gameTime)
        {
            _enemySpawners = EntityHandler.GetWithComponent<EntitySpawnComponent>();

            _player = Globals.PlayerEntity;

            var playerTransform = _player.GetComponent<TransformComponent>();

            _enemySpawners.ForEach(entity =>
            {
                var spawn = entity.GetComponent<EntitySpawnComponent>();

                spawn.SpawnCounter++;

                if (spawn.SpawnInterval <= spawn.SpawnCounter)
                {
                    var transform = entity.GetComponent<TransformComponent>();

                    if (Vector2.Distance(transform.GridPosition.ToVector2(), playerTransform.GridPosition.ToVector2()) > 25)
                    {
                        //new MeleeEnemyEntity(transform.Position);
                    }

                    spawn.SpawnCounter = 0;
                }
            });
        }
    }
}
