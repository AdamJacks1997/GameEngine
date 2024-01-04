using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Globals;

namespace Template.Systems
{
    public class MovementSystem : IUpdateSystem
    {
        private List<Entity> _moveables;
        private List<ColliderComponent> _colliders;
        private List<ColliderComponent> _hurtBoxes;

        private readonly List<Type> _moveableComponentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(VelocityComponent)
        };

        public void Update(GameTime gameTime)
        {
            _moveables = EntityHandler.GetWithComponents(_moveableComponentTypes);

            _moveables.ForEach(moveable =>
            {
                var moveableTransform = moveable.GetComponent<TransformComponent>();
                var moveableVelocity = moveable.GetComponent<VelocityComponent>();

                if (moveableVelocity.DirectionVector == Vector2.Zero)
                {
                    return;
                }

                if (moveable.HasComponent<ColliderComponent>())
                {
                    var moveableCollider = moveable.GetComponent<ColliderComponent>();

                    MoveWithCollisionCheck(moveableTransform, moveableVelocity, moveableCollider, gameTime);
                }
                else
                {
                    MoveWithoutCollisionCheck(moveableTransform, moveableVelocity, gameTime);
                }

                if (moveable.HasComponent<HitBoxComponent>())
                {
                    var moveableHitBox = moveable.GetComponent<HitBoxComponent>();

                    CheckAndResolveHitBoxCollisions(moveable, moveableHitBox);
                }
            });
        }

        private void MoveWithoutCollisionCheck(TransformComponent moveableTransform, VelocityComponent moveableVelocity, GameTime gameTime) // idk if lerp is best for all of these movements
        {
            //moveableTransform.Position += moveableVelocity.DirectionVector * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            var destination = moveableTransform.Position + (moveableVelocity.DirectionVector * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            var distance = moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //moveableTransform.Position.Round();
            moveableTransform.Position = Vector2.Lerp(moveableTransform.Position, destination, distance);
        }

        private void MoveWithCollisionCheck(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider, GameTime gameTime)
        {
            if (moveableVelocity.DirectionVector.X != 0)
            {
                var horizontalMovement = moveableVelocity.DirectionVector.X * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                moveableTransform.Position.X += RoundToOne(horizontalMovement);

                //var destination = new Vector2(moveableTransform.Position.X + (moveableVelocity.DirectionVector.X * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds), moveableTransform.Position.Y);
                //var distance = moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //moveableTransform.Position = Vector2.Lerp(moveableTransform.Position, destination, distance); // Lerp stuff is weird with pathfinding

                _colliders = BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableCollider.Bounds);

                _colliders.ForEach(collider =>
                {
                    if (!moveableCollider.Bounds.Intersects(collider.Bounds))
                    {
                        return;
                    }

                    var intersection = Rectangle.Intersect(moveableCollider.Bounds, collider.Bounds);

                    moveableTransform.Position.X -= RoundToOne(intersection.Width * moveableVelocity.DirectionVector.X);
                });
            }

            if (moveableVelocity.DirectionVector.Y != 0)
            {
                var verticalMovement = moveableVelocity.DirectionVector.Y * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                moveableTransform.Position.Y += RoundToOne(verticalMovement);

                //var destination = new Vector2(moveableTransform.Position.X, moveableTransform.Position.Y + (moveableVelocity.DirectionVector.Y * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds));
                //var distance = moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //moveableTransform.Position = Vector2.Lerp(moveableTransform.Position, destination, distance); // Lerp stuff is weird with pathfinding

                _colliders = BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableCollider.Bounds);

                _colliders.ForEach(collider =>
                {
                    if (!moveableCollider.Bounds.Intersects(collider.Bounds))
                    {
                        return;
                    }

                    var intersection = Rectangle.Intersect(moveableCollider.Bounds, collider.Bounds);

                    moveableTransform.Position.Y -= RoundToOne(intersection.Height * moveableVelocity.DirectionVector.Y);
                });
            }

            moveableTransform.Position.Round();
        }

        private float RoundToOne(float number)
        {
            if (number > 0 && number < 1)
            {
                return 1f;
            }
            else if (number < 0 && number > -1)
            {
                return -1f;
            }
            else
            {
                return (float)Math.Round(number);
            }
        }

        private void CheckAndResolveHitBoxCollisions(Entity moveable, HitBoxComponent moveableHitBox)
        {
            _colliders = BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableHitBox.Bounds);
            _hurtBoxes = BoundaryGroups.HurtBoxBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableHitBox.Bounds);

            _colliders.ForEach(collider =>
            {
                if (!moveableHitBox.Bounds.Intersects(collider.Bounds))
                {
                    return;
                }

                EntityHandler.Remove(moveable);
            });

            _hurtBoxes.ForEach(hurtBox =>
            {
                if (!moveableHitBox.Bounds.Intersects(hurtBox.Bounds))
                {
                    return;
                }

                if (moveableHitBox.Bounds == hurtBox.Bounds)
                {
                    return;
                }

                BoundaryGroups.HitBoxBoundaryHandler.Remove(moveableHitBox);
                BoundaryGroups.HurtBoxBoundaryHandler.Remove(hurtBox);

                EntityHandler.Remove(moveable);
                EntityHandler.Remove(hurtBox.ParentEntity);
            });
        }
    }
}
