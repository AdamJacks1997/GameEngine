using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Components;

namespace Template.Systems
{
    public class MovementSystem : IUpdateSystem
    {
        private List<Entity> _collidables;
        private List<Entity> _moveables;

        private readonly List<Type> _collidableComponentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(ColliderComponent),
        };

        public void Update(GameTime gameTime)
        {
            _collidables = EntityHandler.GetWithComponents(_collidableComponentTypes);
            _moveables = _collidables.Where(c => c.HasComponent<VelocityComponent>()).ToList();

            _moveables.ForEach(moveable =>
            {
                var moveableTransform = moveable.GetComponent<TransformComponent>();
                var moveableVelocity = moveable.GetComponent<VelocityComponent>();
                var moveableCollider = moveable.GetComponent<ColliderComponent>();

                if (moveableVelocity.DirectionVector == Vector2.Zero)
                {
                    return;
                }

                SetNewPosition(moveableTransform, moveableVelocity, moveableCollider, gameTime);

                _collidables.ForEach(collidable =>
                {
                    if (moveable == collidable)
                    {
                        return;
                    }

                    var collidableTransform = collidable.GetComponent<TransformComponent>();

                    if (!IsCollidableInRange(moveableTransform, collidableTransform))
                    {
                        return;
                    }

                    var collidableCollider = collidable.GetComponent<ColliderComponent>();

                    if (!moveableCollider.Bounds.Intersects(collidableCollider.Bounds))
                    {
                        return;
                    }

                    ResolveCollision(moveableTransform, moveableVelocity, moveableCollider, collidableCollider);
                });

                ConfirmNewPosition(moveableTransform, moveableVelocity, moveableCollider);
            });
        }

        private void SetNewPosition(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider, GameTime gameTime)
        {
            moveableTransform.Position.X += (moveableVelocity.DirectionVector.X * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            moveableTransform.Position.Y += (moveableVelocity.DirectionVector.Y * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
        }

        private bool IsCollidableInRange(TransformComponent moveableTransform, TransformComponent collidableTransform)
        {
            if (Vector2.Distance(moveableTransform.Position, collidableTransform.Position) > 32)
            {
                return false; // return if distance is more than 32px away
            }

            return true;
        }

        private void ResolveCollision(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider, ColliderComponent collidableCollider)
        {
            var intersectionVector = GetIntersectionRectangle(moveableCollider.Bounds, collidableCollider.Bounds);

            if (intersectionVector.X == intersectionVector.Y)
            {
                intersectionVector = new Vector2(0, intersectionVector.Y - 1);
            }
            else
            {
                intersectionVector = Math.Min(intersectionVector.X, intersectionVector.Y) == intersectionVector.X ?
                new Vector2(intersectionVector.X + 1, 0) :
                new Vector2(0, intersectionVector.Y + 1);
            }

            moveableTransform.Position.X -= (float)Math.Round(intersectionVector.X * moveableVelocity.DirectionVector.X);
            moveableTransform.Position.Y -= (float)Math.Round(intersectionVector.Y * moveableVelocity.DirectionVector.Y);
            moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
        }

        private Vector2 GetIntersectionRectangle(Rectangle moveableBounds, Rectangle collidableBounds)
        {
            var intercection = Rectangle.Intersect(moveableBounds, collidableBounds);

            return new Vector2(intercection.Width, intercection.Height);
        }

        private void ConfirmNewPosition(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider)
        {
            moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X);
            moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y);
            moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
        }
    }
}
