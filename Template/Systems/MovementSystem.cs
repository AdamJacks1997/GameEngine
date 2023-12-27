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

                _collidables = _collisionHandler.CollisionQuadtree.FindCollisions(moveableCollider.Bounds);

                var topLeft = Rectangle.Empty;
                var topRight = Rectangle.Empty;

                var bottomLeft = Rectangle.Empty;
                var bottomRight = Rectangle.Empty;

                var topLeftCollides = false;
                var topRightCollides = false;

                var bottomLeftCollides = false;
                var bottomRightCollides = false;

                var topLeftIntersection = Rectangle.Empty;
                var topRightIntersection = Rectangle.Empty;

                var bottomLeftIntersection = Rectangle.Empty;
                var bottomRightIntersection = Rectangle.Empty;

                _collidables.ForEach(collidable =>
                {
                    if (!collidable.HasComponent<TileComponent>())
                    {
                        return;
                    }

                    var collidableTransform = collidable.GetComponent<TransformComponent>();
                    var collidableCollider = collidable.GetComponent<ColliderComponent>();

                    if (!moveableCollider.Bounds.Intersects(collidableCollider.Bounds))
                    {
                        return;
                    }

                    topLeft = new Rectangle(moveableCollider.Bounds.Left, moveableCollider.Bounds.Top, GameSettings.TileSize / 2, GameSettings.TileSize / 2);
                    topRight = new Rectangle(moveableCollider.Bounds.Right - GameSettings.TileSize / 2, moveableCollider.Bounds.Top, GameSettings.TileSize / 2, GameSettings.TileSize / 2);

                    bottomLeft = new Rectangle(moveableCollider.Bounds.Left, moveableCollider.Bounds.Bottom - GameSettings.TileSize / 2, GameSettings.TileSize / 2, GameSettings.TileSize / 2);
                    bottomRight = new Rectangle(moveableCollider.Bounds.Right - GameSettings.TileSize / 2, moveableCollider.Bounds.Bottom - GameSettings.TileSize / 2, GameSettings.TileSize / 2, GameSettings.TileSize / 2);

                    topLeftCollides = topLeft.Intersects(collidableCollider.Bounds) ? true : topLeftCollides;
                    topRightCollides = topRight.Intersects(collidableCollider.Bounds) ? true : topRightCollides;

                    bottomLeftCollides = bottomLeft.Intersects(collidableCollider.Bounds) ? true : bottomLeftCollides;
                    bottomRightCollides = bottomRight.Intersects(collidableCollider.Bounds) ? true : bottomRightCollides;

                    if (topLeftCollides)
                    {
                        var newTopLeftIntersection = Rectangle.Intersect(topLeft, collidableCollider.Bounds);

                        if (newTopLeftIntersection.Width > topLeftIntersection.Width)
                        {
                            topLeftIntersection.Width = newTopLeftIntersection.Width;
                        }

                        if (newTopLeftIntersection.Height > topLeftIntersection.Height)
                        {
                            topLeftIntersection.Height = newTopLeftIntersection.Height;
                        }
                    }

                    if (topRightCollides)
                    {
                        var newTopRightIntersection = Rectangle.Intersect(topRight, collidableCollider.Bounds);

                        if (newTopRightIntersection.Width > topRightIntersection.Width)
                        {
                            topRightIntersection.Width = newTopRightIntersection.Width;
                        }

                        if (newTopRightIntersection.Height > topRightIntersection.Height)
                        {
                            topRightIntersection.Height = newTopRightIntersection.Height;
                        }
                    }

                    if (bottomLeftCollides)
                    {
                        var newBottomLeftIntersection = Rectangle.Intersect(bottomLeft, collidableCollider.Bounds);

                        if (newBottomLeftIntersection.Width > bottomLeftIntersection.Width)
                        {
                            bottomLeftIntersection.Width = newBottomLeftIntersection.Width;
                        }

                        if (newBottomLeftIntersection.Height > bottomLeftIntersection.Height)
                        {
                            bottomLeftIntersection.Height = newBottomLeftIntersection.Height;
                        }
                    }

                    if (bottomRightCollides)
                    {
                        var newBottomRightIntersection = Rectangle.Intersect(bottomRight, collidableCollider.Bounds);

                        if (newBottomRightIntersection.Width > bottomRightIntersection.Width)
                        {
                            bottomRightIntersection.Width = newBottomRightIntersection.Width;
                        }

                        if (newBottomRightIntersection.Height > bottomRightIntersection.Height)
                        {
                            bottomRightIntersection.Height = newBottomRightIntersection.Height;
                        }
                    }
                });

                if (topLeftCollides && topRightCollides && (!bottomLeftCollides && !bottomRightCollides))
                {
                    moveableTransform.Position.Y += topLeftIntersection.Height;
                }

                if (bottomLeftCollides && bottomRightCollides && (!topLeftCollides && !topRightCollides))
                {
                    moveableTransform.Position.Y -= bottomLeftIntersection.Height;
                }

                if (topLeftCollides && bottomLeftCollides && (!topRightCollides && !bottomRightCollides))
                {
                    moveableTransform.Position.X += topLeftIntersection.Width;
                }

                if (topRightCollides && bottomRightCollides && (!topLeftCollides && !bottomLeftCollides))
                {
                    moveableTransform.Position.X -= topRightIntersection.Width;
                }

                if (topLeftCollides && (!topRightCollides && !bottomLeftCollides))
                {
                    if (topLeftIntersection.Height > topLeftIntersection.Width)
                    {
                        moveableTransform.Position.X += topLeftIntersection.Width;
                    }
                    else
                    {
                        moveableTransform.Position.Y += topLeftIntersection.Height;
                    }
                }

                if (topRightCollides && (!topLeftCollides && !bottomRightCollides))
                {
                    if (topRightIntersection.Height > topRightIntersection.Width)
                    {
                        moveableTransform.Position.X -= topRightIntersection.Width;
                    }
                    else
                    {
                        moveableTransform.Position.Y += topRightIntersection.Height;
                    }
                }

                if (bottomLeftCollides && (!bottomRightCollides && !topLeftCollides))
                {
                    if (bottomLeftIntersection.Height > bottomLeftIntersection.Width)
                    {
                        moveableTransform.Position.X += bottomLeftIntersection.Width;
                    }
                    else
                    {
                        moveableTransform.Position.Y -= bottomLeftIntersection.Height;
                    }
                }

                if (bottomRightCollides && (!bottomLeftCollides && !topRightCollides))
                {
                    if (bottomRightIntersection.Height > bottomRightIntersection.Width)
                    {
                        moveableTransform.Position.X -= bottomRightIntersection.Width;
                    }
                    else
                    {
                        moveableTransform.Position.Y -= bottomRightIntersection.Height;
                    }
                }

                if (topLeftCollides && (topRightCollides && bottomLeftCollides))
                {
                    moveableTransform.Position.X += 2;
                    moveableTransform.Position.Y += 2;
                }

                if (topRightCollides && (topLeftCollides && bottomRightCollides))
                {
                    moveableTransform.Position.X -= 2;
                    moveableTransform.Position.Y += 2;
                }

                if (bottomLeftCollides && (bottomRightCollides && topLeftCollides))
                {
                    moveableTransform.Position.X += 2;
                    moveableTransform.Position.Y -= 2;
                }

                if (bottomRightCollides && (bottomLeftCollides && topRightCollides))
                {
                    moveableTransform.Position.X -= - 2;
                    moveableTransform.Position.Y -= 2;
                }

                moveableTransform.Position.Round();

                moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
                moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
            });
        }

        private void SetNewPosition(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider, GameTime gameTime)
        {
            moveableTransform.Position.X += (float)Math.Round(moveableVelocity.DirectionVector.X * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            moveableTransform.Position.Y += (float)Math.Round(moveableVelocity.DirectionVector.Y * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
        }

        private Vector2 GetIntersectionRectangle(Rectangle moveableBounds, Rectangle collidableBounds)
        {
            var intercection = Rectangle.Intersect(moveableBounds, collidableBounds);

            return new Vector2(intercection.Width, intercection.Height);
        }
    }
}
