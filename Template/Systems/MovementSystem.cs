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

                    //ResolveCollision(moveableTransform, moveableVelocity, moveableCollider, collidableCollider);
                    //ResolveCollisionTest(moveableTransform, moveableVelocity, moveableCollider, collidableCollider);


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
                    moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y + topLeftIntersection.Height);
                }

                if (bottomLeftCollides && bottomRightCollides && (!topLeftCollides && !topRightCollides))
                {
                    moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y - bottomLeftIntersection.Height);
                }

                if (topLeftCollides && bottomLeftCollides && (!topRightCollides && !bottomRightCollides))
                {
                    moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X + topLeftIntersection.Width);
                }

                if (topRightCollides && bottomRightCollides && (!topLeftCollides && !bottomLeftCollides))
                {
                    moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X - topRightIntersection.Width);
                }

                if (topLeftCollides && (!topRightCollides && !bottomLeftCollides))
                {
                    if (topLeftIntersection.Height > topLeftIntersection.Width)
                    {
                        moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X + topLeftIntersection.Width);
                    }
                    else
                    {
                        moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y + topLeftIntersection.Height);
                    }
                }

                if (topRightCollides && (!topLeftCollides && !bottomRightCollides))
                {
                    if (topRightIntersection.Height > topRightIntersection.Width)
                    {
                        moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X - topRightIntersection.Width);
                    }
                    else
                    {
                        moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y + topRightIntersection.Height);
                    }
                }

                if (bottomLeftCollides && (!bottomRightCollides && !topLeftCollides))
                {
                    if (bottomLeftIntersection.Height > bottomLeftIntersection.Width)
                    {
                        moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X + bottomLeftIntersection.Width);
                    }
                    else
                    {
                        moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y - bottomLeftIntersection.Height);
                    }
                }

                if (bottomRightCollides && (!bottomLeftCollides && !topRightCollides))
                {
                    if (bottomRightIntersection.Height > bottomRightIntersection.Width)
                    {
                        moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X - bottomRightIntersection.Width);
                    }
                    else
                    {
                        moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y - bottomRightIntersection.Height);
                    }
                }

                if (topLeftCollides && (topRightCollides && bottomLeftCollides))
                {
                    moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X + 2);
                    moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y + 2);
                }

                if (topRightCollides && (topLeftCollides && bottomRightCollides))
                {
                    moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X - 2);
                    moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y + 2);
                }

                if (bottomLeftCollides && (bottomRightCollides && topLeftCollides))
                {
                    moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X + 2);
                    moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y - 2);
                }

                if (bottomRightCollides && (bottomLeftCollides && topRightCollides))
                {
                    moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X - 2);
                    moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y - 2);
                }

                moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
                moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;

                ConfirmNewPosition(moveableTransform, moveableVelocity, moveableCollider);
            });
        }

        private void SetNewPosition(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider, GameTime gameTime)
        {
            moveableTransform.Position.X += (float)Math.Round(moveableVelocity.DirectionVector.X * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            moveableTransform.Position.Y += (float)Math.Round(moveableVelocity.DirectionVector.Y * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
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

        private void ResolveCollisionTest(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider, ColliderComponent collidableCollider)
        {
            var newPositionOffset = Vector2.Zero;

            bool collidesOnLeft = moveableCollider.Bounds.Left <= collidableCollider.Bounds.Right && moveableCollider.Bounds.Left > collidableCollider.Bounds.Left;
            bool collidesOnRight = moveableCollider.Bounds.Right >= collidableCollider.Bounds.Left;

            bool collidesOnTop = moveableCollider.Bounds.Top <= collidableCollider.Bounds.Bottom && moveableCollider.Bounds.Top > collidableCollider.Bounds.Top;
            bool collidesOnBottom = moveableCollider.Bounds.Bottom >= collidableCollider.Bounds.Top;

            if (moveableVelocity.DirectionVector.X != 0)
            {
                if (collidesOnLeft)
                {
                    newPositionOffset.X = collidableCollider.Bounds.Right - moveableCollider.Bounds.Left;
                }
                else if (collidesOnRight)
                {
                    newPositionOffset.X = collidableCollider.Bounds.Left - moveableCollider.Bounds.Right;
                }
            }

            if (moveableVelocity.DirectionVector.Y != 0)
            {
                if (collidesOnTop)
                {
                    newPositionOffset.Y = collidableCollider.Bounds.Bottom - moveableCollider.Bounds.Top;
                }
                else if (collidesOnBottom)
                {
                    newPositionOffset.Y = collidableCollider.Bounds.Top - moveableCollider.Bounds.Bottom;
                }
            }

            // these can collide with multiple rects at the same time
            // I need to take into considerationtion all collided before moving the player back
            //var topLeft = new Rectangle(moveableCollider.Bounds.Left, moveableCollider.Bounds.Top, 1, 1);
            //var topRight = new Rectangle(moveableCollider.Bounds.Right - 1, moveableCollider.Bounds.Top, 1, 1);

            //var topLeftCollides = topLeft.Intersects(collidableCollider.Bounds);
            //var topRightCollides = topRight.Intersects(collidableCollider.Bounds);

            // Maybe this can work:
            // https://community.monogame.net/t/solved-how-would-i-go-about-sliding-a-collision-while-moving-diagonally/11343/2

            if (newPositionOffset.X != 0 && newPositionOffset.Y != 0)
            {
                var newHorizontalRectangle = new Rectangle((int)Math.Round(moveableCollider.Bounds.X + newPositionOffset.X), moveableCollider.Bounds.Y, moveableCollider.Bounds.Width, moveableCollider.Bounds.Height);
                var newVerticalRectangle = new Rectangle(moveableCollider.Bounds.X, (int)Math.Round(moveableCollider.Bounds.Y + newPositionOffset.Y), moveableCollider.Bounds.Width, moveableCollider.Bounds.Height);

                var newHorizontalRectangleCollides = newHorizontalRectangle.Intersects(collidableCollider.Bounds); // THESE ARE ALWAYS FALSE, WHY?!?!!
                var newVerticalRectangleCollides = newVerticalRectangle.Intersects(collidableCollider.Bounds);

                if (newHorizontalRectangleCollides)
                {
                    newPositionOffset.X = 0;
                }

                if (newVerticalRectangleCollides)
                {
                    newPositionOffset.Y = 0;
                }
            }

            if (newPositionOffset == Vector2.Zero)
            {
                return;
            }

            moveableTransform.Position = new Vector2((float)Math.Round(moveableTransform.Position.X + newPositionOffset.X), (float)Math.Round(moveableTransform.Position.Y + newPositionOffset.Y));

            moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;

            //moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X);
            //moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y);
            //moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            //moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
        }

        private Vector2 GetIntersectionRectangle(Rectangle moveableBounds, Rectangle collidableBounds)
        {
            var intercection = Rectangle.Intersect(moveableBounds, collidableBounds);

            return new Vector2(intercection.Width, intercection.Height);
        }

        private void ConfirmNewPosition(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider)
        {
            //moveableTransform.Position.X = (float)Math.Round(moveableTransform.Position.X);
            //moveableTransform.Position.Y = (float)Math.Round(moveableTransform.Position.Y);
            //moveableCollider.Bounds.X = (int)moveableTransform.Position.X + moveableCollider.Offset.X;
            //moveableCollider.Bounds.Y = (int)moveableTransform.Position.Y + moveableCollider.Offset.Y;
        }
    }
}
