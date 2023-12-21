using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Components;
using Template.Handlers;
using GameEngine.Constants;

namespace Template.Systems
{
    public class MovementSystem : IUpdateSystem
    {
        private readonly CollisionHandler _collisionHandler;

        private List<Entity> _moveables;
        private List<Entity> _collidables;

        private readonly List<Type> _moveableComponentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(ColliderComponent),
            typeof(VelocityComponent)
        };

        public MovementSystem(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }

        public void Update(GameTime gameTime)
        {
            _moveables = EntityHandler.GetWithComponents(_moveableComponentTypes);

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

                _collidables = _collisionHandler.CollisionQuadtree.FindCollisions(moveable);

                _collidables.ForEach(collidable =>
                {
                    if (moveable == collidable)
                    {
                        return;
                    }

                    var collidableTransform = collidable.GetComponent<TransformComponent>();
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
            moveableTransform.Position.X += moveableVelocity.DirectionVector.X * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveableTransform.Position.Y += moveableVelocity.DirectionVector.Y * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
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

                if (moveableVelocity.DirectionVector.X == 0 && intersectionVector.Y == 0 && (moveableCollider.Bounds.X < collidableCollider.Bounds.X + GameSettings.TileSize || moveableCollider.Bounds.X + GameSettings.TileSize > collidableCollider.Bounds.X))
                {
                    if (moveableCollider.Bounds.X < collidableCollider.Bounds.X + GameSettings.TileSize) // collision on left
                    {
                        var collideFromTopSide = moveableCollider.Bounds.X - collidableCollider.Bounds.X + GameSettings.TileSize;

                        var collideFromBottomSide = collidableCollider.Bounds.X - moveableCollider.Bounds.X + GameSettings.TileSize;

                        if (collideFromTopSide > collideFromBottomSide)
                        {
                            moveableVelocity.DirectionVector.X = -1; // left side
                        }

                        if (collideFromBottomSide > collideFromTopSide) // right side
                        {
                            moveableVelocity.DirectionVector.X = 1;
                        }
                    }
                }

                if (moveableVelocity.DirectionVector.Y == 0 && intersectionVector.X == 0 && (moveableCollider.Bounds.Y < collidableCollider.Bounds.Y + GameSettings.TileSize || moveableCollider.Bounds.Y + GameSettings.TileSize > collidableCollider.Bounds.Y))
                {
                    if (moveableCollider.Bounds.Y < collidableCollider.Bounds.Y + GameSettings.TileSize) // collision on left
                    {
                        var collideFromTopSide = moveableCollider.Bounds.Y - collidableCollider.Bounds.Y + GameSettings.TileSize;

                        var collideFromBottomSide = collidableCollider.Bounds.Y - moveableCollider.Bounds.Y + GameSettings.TileSize;

                        if (collideFromTopSide > collideFromBottomSide)
                        {
                            moveableVelocity.DirectionVector.Y = -1; // bottom side
                        }

                        if (collideFromBottomSide > collideFromTopSide) // top side
                        {
                            moveableVelocity.DirectionVector.Y = 1;
                        }
                    }
                }
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
