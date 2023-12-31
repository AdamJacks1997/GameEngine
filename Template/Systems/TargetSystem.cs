using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Globals;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using GameEngineTools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Template.Components;

namespace Template.Systems
{
    public class TargetSystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private Entity _player;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(BrainComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _player = Globals.PlayerEntity;

            var playerTransform = _player.GetComponent<TransformComponent>();

            _entities.ForEach(entity =>
            {
                var brain = entity.GetComponent<BrainComponent>();
                var pathController = entity.GetComponent<PathControllerComponent>();
                var transform = entity.GetComponent<TransformComponent>();

                // Path to Player
                if (Vector2.Distance(transform.GridPosition.ToVector2(), playerTransform.GridPosition.ToVector2()) <= brain.PathStartDistance) 
                {
                    pathController.Destination = playerTransform.Position;

                    return;
                }

                // Wander
                if (brain.WanderDestinationChangeCounter >= brain.WanderDestinationChangeInterval) // Maybe I should make a WanderSystem?
                {
                    var randomGridPosition = GetRandomFloorTileGridPosition(transform.GridPosition, brain.WanderRange);

                    pathController.Destination = randomGridPosition.ToVector2() * GameSettings.TileSize;

                    brain.WanderDestinationChangeCounter = 0;
                }

                brain.WanderDestinationChangeCounter++;
            });
        }

        private Point GetRandomFloorTileGridPosition(Point gridPosition, int wanderRange)
        {
            var newGridPosition = Randoms.PointWithinRadius(gridPosition, wanderRange);

            if (newGridPosition.X < -20000)
            {
                var test = "";
            }

            if (newGridPosition.Y < -20000)
            {
                var test = "";
            }

            if (newGridPosition.X > Globals.CurrentCollisions.Length || newGridPosition.X < 0)
            {
                newGridPosition.X = Globals.CurrentCollisions.Length - 1;
            }

            if (newGridPosition.Y > Globals.CurrentCollisions[newGridPosition.X].Length || newGridPosition.Y < 0)
            {
                newGridPosition.Y = Globals.CurrentCollisions[newGridPosition.X].Length - 1;
            }

            if (Globals.CurrentCollisions[newGridPosition.X][newGridPosition.Y] == 1)
            {
                return GetRandomFloorTileGridPosition(gridPosition, wanderRange);
            }

            return newGridPosition;
        }
    }
}
