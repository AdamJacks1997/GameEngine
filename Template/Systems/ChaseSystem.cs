using GameEngine.Components;
using GameEngine.Enums;
using GameEngine.Globals;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Monogame;
using GameEngine.Renderers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Template.Components;

namespace Template.Systems
{
    public class ChaseSystem : IUpdateSystem, IDrawSystem
    {
        private List<Entity> _entities;

        private int testInterval = 0;

        private float _minimumDistance = GameSettings.TileSize * 2;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(BrainComponent),
        };

        public void Update(GameTime gameTime)
        {
            //if (testInterval < 4) // FOR TESTING
            //{
            //    testInterval++;

            //    return;
            //}

            //testInterval = 0;

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








                var directionToTarget = targetTranform.Position - transform.Position;

                directionToTarget = directionToTarget.NormalizeWithZeroCheck();

                velocity.DirectionVector = directionToTarget;






                //    var colliders = BoundaryGroups.MovableBoundaryHandler.BoundaryQuadtree.FindCollisions(collider.Bounds, 40);

                //    colliders.AddRange(BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(collider.Bounds, 5));

                //    float[] dangers = new float[8];
                //    float[] interests = new float[8];
                //    float[] strafes = new float[8];

                //    colliders.ForEach(moveable =>
                //    {
                //        if (checkedEntities.Contains(moveable.ParentEntity))
                //        {
                //            return;
                //        }

                //        if (moveable.ParentEntity == Globals.PlayerEntity)
                //        {
                //            return;
                //        }

                //        if (collider == moveable)
                //        {
                //            return;
                //        }

                //        Vector2 directionToObstacle = moveable.Bounds.Location.ToVector2() - transform.Position;

                //        directionToObstacle = directionToObstacle.NormalizeWithZeroCheck();

                //        //var distanceToObstacleMagnitude = directionToObstacle.Length();
                //        var distanceToObstacle = Vector2.Distance(moveable.Bounds.Location.ToVector2(), transform.Position);

                //        float weight = -0.2f;

                //        if (distanceToObstacle <= GameSettings.TileSize * 1)
                //        {
                //            weight = -0.8f;
                //        }

                //        for (int i = 0; i < CheckDirections.eightDirections.Count; i++)
                //        {
                //            float result = Vector2.Dot(directionToObstacle, CheckDirections.eightDirections[i]);

                //            float weightedResult = result * weight;

                //            //override value only if it is higher than the current one stored in the danger array
                //            if (weightedResult > dangers[i])
                //            {
                //                dangers[i] = weightedResult;
                //            }
                //        }
                //    });

                //    var directionToTarget = targetTranform.Position - transform.Position;
                //    var distanceToTarget = Vector2.Distance(targetTranform.Position, transform.Position);

                //    directionToTarget = directionToTarget.NormalizeWithZeroCheck();

                //    var displacement = directionToTarget;

                //    if (distanceToTarget <= _minimumDistance)
                //    {
                //        displacement = directionToTarget * -1f; // if too close, set desired direction away from player
                //    }

                //    for (int i = 0; i < interests.Length; i++)
                //    {
                //        float dot = Vector2.Dot(displacement, CheckDirections.eightDirections[i]);

                //        //float weight = dot;

                //        float weight = dot;

                //        if (distanceToTarget <= GameSettings.TileSize * 3)
                //        {
                //            //weight = 1 - MathF.Pow(MathF.Abs(dot + 0.25f), 2.0f);
                //            //weight = 0;
                //            weight = dot - 1;
                //        }

                //        //weight = 1.0f - MathF.Pow(MathF.Abs(result + 0.5f), 2.0f); 
                //        //weight = result - 0.96f;

                //        //float weightedResult = weight;
                //        //float weightedResult = result * weight;

                //        // accept only directions at the less than 90 degrees to the target direction
                //        if (weight > 0)
                //        {
                //            interests[i] = weight;
                //        }
                //    }

                //    for (int i = 0; i < 8; i++)
                //    {
                //        interests[i] = Math.Clamp(interests[i] + dangers[i], 0, 1);
                //    }

                //    Vector2 finalDirection = new Vector2();

                //    for (int i = 0; i < 8; i++)
                //    {
                //        finalDirection += CheckDirections.eightDirections[i] * interests[i];

                //        finalDirection = finalDirection.NormalizeWithZeroCheck();
                //    }

                //    finalDirection += velocity.DirectionVector;

                //    velocity.DirectionVector = Vector2.Lerp(velocity.DirectionVector, finalDirection, 1f);

                //    velocity.DirectionVector = velocity.DirectionVector.NormalizeWithZeroCheck();
            });
        }

        public void Draw()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();

                //Globals.SpriteBatch.DrawLine(transform.MidPosition.X, transform.MidPosition.Y, transform.MidPosition.X + (velocity.DirectionVector.X * 20), transform.MidPosition.Y + (velocity.DirectionVector.Y * 20), Color.Purple);
            });

            //Globals.SpriteBatch.DrawCircle(Globals.PlayerEntity.Transform.MidPosition, _minimumDistance, 60, Color.Red);

            //Globals.SpriteBatch.DrawCircle(Globals.PlayerEntity.Transform.MidPosition, GameSettings.TileSize * 3, 60, Color.Green);
        }

        private class CheckDirections
        {
            public static List<Vector2> eightDirections = new List<Vector2>{
                Normalized(new Vector2(0,1)),
                Normalized(new Vector2(1,1)),
                Normalized(new Vector2(1,0)),
                Normalized(new Vector2(1,-1)),
                Normalized(new Vector2(0,-1)),
                Normalized(new Vector2(-1,-1)),
                Normalized(new Vector2(-1,0)),
                Normalized(new Vector2(-1,1))
            };

            private static Vector2 Normalized(Vector2 vector)
            {
                vector.Normalize();

                return vector;
            }
        }
    }
}
