using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Enums;
using GameEngine.Globals;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Renderers;
using GameEngine.Systems;
using GameEngineTools;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Template.Components;

namespace Template.Systems
{
    public class ChaseSystem : IUpdateSystem, IDrawSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(BrainComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            var checkedEntities = new List<Entity>();

            _entities.ForEach(entity =>
            {
                var brain = entity.GetComponent<BrainComponent>();

                if (brain.State != EntityStateEnum.Chase)
                {
                    return;
                }

                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();
                var collider = entity.GetComponent<ColliderComponent>();

                var targetEntity = brain.Target;
                var targetTranform = targetEntity.GetComponent<TransformComponent>();
                var targetVelocity = targetEntity.GetComponent<VelocityComponent>();

                var targetFuturePosition = targetTranform.Position + (targetVelocity.DirectionVector * 10f);

                var desired = Vector2.Normalize(targetFuturePosition - transform.Position);

                float desiredDot = Vector2.Dot(desired, velocity.DirectionVector); // used to stop enemies going straight at the target

                var steering = desired - velocity.DirectionVector * Math.Abs(desiredDot - 0.1f);

                Vector2 avoidanceForce = Vector2.Zero;

                var movableColliders = BoundaryGroups.MovableBoundaryHandler.BoundaryQuadtree.FindCollisions(collider.Bounds);

                movableColliders.ForEach(moveable =>
                {
                    if (checkedEntities.Contains(moveable.ParentEntity))
                    {
                        return;
                    }

                    if (moveable.ParentEntity == Globals.PlayerEntity)
                    {
                        return;
                    }

                    if (collider == moveable)
                    {
                        return;
                    }

                    var toObstacle = moveable.Bounds.Location.ToVector2() - transform.Position;

                    if (toObstacle == Vector2.Zero)
                    {
                        return; // Shit bug fix for something that should never happen
                    }

                    var toObstacleNormalized = Vector2.Normalize(toObstacle);

                    float obstacleDot = Vector2.Dot(toObstacleNormalized, velocity.DirectionVector);

                    avoidanceForce -= toObstacleNormalized * 0.05f * Math.Abs(obstacleDot - 0.5f);
                });

                if (avoidanceForce != Vector2.Zero)
                {
                    steering += Vector2.Normalize(avoidanceForce);
                }

                velocity.DirectionVector += steering;


                checkedEntities.Add(entity); // super inefficient but good for testing :D


                // ----------- Old chase stuff

                //var targetEntity = brain.Target;

                //var targetTranform = targetEntity.GetComponent<TransformComponent>();

                //velocity.DirectionVector = -Vector2.Normalize(transform.Position - targetTranform.Position);

                //if (velocity.DirectionVector.X != 0 && velocity.DirectionVector.Y != 0)
                //{
                //    velocity.DirectionVector *= new Vector2(GameSettings.DiagnalSpeedMultiplier);
                //}
            });
        }

        public void Draw()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();

                Globals.SpriteBatch.DrawLine(transform.Position.X, transform.Position.Y, transform.Position.X + (velocity.DirectionVector.X * 100f), transform.Position.Y + (velocity.DirectionVector.Y * 100f), Color.Purple);
            });
        }
    }
}
