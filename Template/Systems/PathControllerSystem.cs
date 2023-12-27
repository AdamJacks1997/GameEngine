using System;
using System.Collections.Generic;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Template.Components;
using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Renderers;
using GameEngine.Globals;
using GameEngine.Enums;

namespace Template.Systems
{
    public class PathControllerSystem : IUpdateSystem, IDrawSystem
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
                var sprite = entity.GetComponent<SpriteComponent>();

                if (pathController.Destination == Vector2.Zero)
                {
                    return;
                }

                if (brain.State != EntityStateEnum.Wander && brain.State != EntityStateEnum.FollowPath)
                {
                    return;
                }

                var isWithinPathStartDistance = Vector2.Distance(transform.GridPosition.ToVector2(), pathController.GridDestination.ToVector2()) <= brain.PathStartDistance;

                if (pathController.CurrentPath == null)
                {
                    pathController.CurrentPath = pathController.PathHandler.FindPath(transform.GridPosition, pathController.GridDestination);

                    return;
                }

                pathController.PathRefreshCounter++;

                if (pathController.PathRefreshCounter >= pathController.PathRefreshInterval && isWithinPathStartDistance) // Might be better if this is only ran when outside of stop distance
                {
                    pathController.CurrentPath = pathController.PathHandler.FindPath(transform.GridPosition, pathController.GridDestination);

                    pathController.PathRefreshCounter = 0;
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
                    var distanceFromTargetTile = Vector2.Distance(currentTargetTilePixelPosition, transform.Position);

                    if (distanceFromTargetTile > 1.5)
                    {
                        velocity.DirectionVector = currentTargetTilePixelPosition - transform.Position;

                        if (velocity.DirectionVector != Vector2.Zero)
                        {
                            velocity.DirectionVector.Normalize();

                            if (velocity.DirectionVector.X != 0 && velocity.DirectionVector.Y != 0)
                            {
                                velocity.DirectionVector *= new Vector2(GameSettings.DiagnalSpeedMultiplier, GameSettings.DiagnalSpeedMultiplier);
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

        public void Draw()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var pathController = entity.GetComponent<PathControllerComponent>();

                if (pathController.CurrentPath == null || pathController.CurrentPath?.Count < 1)
                {
                    return;
                }

                pathController.CurrentPath.ForEach(path =>
                {
                    var pathRectangle = new Rectangle(path.X * GameSettings.TileSize, path.Y * GameSettings.TileSize, GameSettings.TileSize, GameSettings.TileSize);

                    Globals.SpriteBatch.DrawRectangle(pathRectangle, Color.Green);
                });

                var currentTargetTileRectangle = new Rectangle(pathController.CurrentPath[0].X * GameSettings.TileSize, pathController.CurrentPath[0].Y * GameSettings.TileSize, GameSettings.TileSize, GameSettings.TileSize);

                Globals.SpriteBatch.DrawRectangle(currentTargetTileRectangle, Color.Red);
            });
        }
    }
}
