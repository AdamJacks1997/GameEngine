using System;
using System.Collections.Generic;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Template.Components;
using GameEngine.Components;
using GameEngine.Globals;
using GameEngine.Enums;
using GameEngine.Monogame;

namespace Template.Systems
{
    public class PathFollowSystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(PathControllerComponent),
            typeof(BrainComponent),
            typeof(TransformComponent),
            typeof(VelocityComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var pathController = entity.GetComponent<PathControllerComponent>();
                var brain = entity.GetComponent<BrainComponent>();
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();

                if (pathController.Destination == Vector2.Zero)
                {
                    return;
                }

                if (brain.State != EntityStateEnum.Wander && brain.State != EntityStateEnum.FollowPath)
                {
                    return;
                }

                if (pathController.CurrentPath.Count < 1)
                {
                    velocity.DirectionVector = Vector2.Zero;

                    return;
                }

                var currentTargetTile = pathController.CurrentPath[0].ToVector2();

                var isCloserThanPathStopDistance = brain.PathStopDistance < Vector2.Distance(currentTargetTile, pathController.GridDestination.ToVector2());

                if (isCloserThanPathStopDistance || brain.State == EntityStateEnum.Wander)
                {
                    var currentTargetTilePixelPosition = (currentTargetTile * new Vector2(GameSettings.TileSize));

                    //currentTargetTilePixelPosition.Y -= 1; // Due to collision boxes being 1 pixel taller

                    var distanceFromTargetTile = Vector2.Distance(currentTargetTilePixelPosition, transform.Position);

                    if (distanceFromTargetTile > 0)
                    {
                        velocity.DirectionVector = currentTargetTilePixelPosition - transform.Position;

                        if (velocity.DirectionVector != Vector2.Zero)
                        {
                            velocity.DirectionVector = velocity.DirectionVector.NormalizeWithZeroCheck();

                            if (velocity.DirectionVector.X != 0 && velocity.DirectionVector.Y != 0)
                            {
                                velocity.DirectionVector *= new Vector2(GameSettings.DiagnalSpeedMultiplier);

                                var test = "";
                            }
                        }
                    }
                    else
                    {
                        pathController.CurrentPath.RemoveAt(0);
                    }
                }
                else
                {
                    velocity.DirectionVector = Vector2.Zero;
                }
            });
        }
    }
}
