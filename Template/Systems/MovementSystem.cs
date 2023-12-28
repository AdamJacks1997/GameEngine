using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Globals;
using Template.Components;

namespace Template.Systems
{
    public class MovementSystem : IUpdateSystem
    {
        private readonly BoundaryHandler _tileBoundaryHandler;
        private readonly BoundaryHandler _attackBoundaryHandler;

        private List<Entity> _moveables;
        private List<Rectangle> _bounaries;
        private List<Rectangle> _attackBoxes;

        private readonly List<Type> _moveableComponentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(VelocityComponent)
        };

        public MovementSystem(BoundaryHandler tileBoundaryHandler, BoundaryHandler attackBoundaryHandler)
        {
            _tileBoundaryHandler = tileBoundaryHandler;
            _attackBoundaryHandler = attackBoundaryHandler;
        }

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

                SetNewPosition(moveableTransform, moveableVelocity, gameTime);

                if (moveable.HasComponent<ColliderComponent>())
                {
                    var moveableCollider = moveable.GetComponent<ColliderComponent>();

                    CheckAndResolveTileCollisions(moveableTransform, moveableCollider);
                }

                if (moveable.HasComponent<AttackBoxComponent>() && moveable.HasComponent<AttackComponent>())
                {
                    var moveableAttackBox = moveable.GetComponent<AttackBoxComponent>();

                    CheckAndResolveAttackBoxCollisions(moveable, moveableAttackBox);
                }
            });
        }

        private void SetNewPosition(TransformComponent moveableTransform, VelocityComponent moveableVelocity, GameTime gameTime)
        {
            moveableTransform.Position.X += (float)Math.Round(moveableVelocity.DirectionVector.X * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            moveableTransform.Position.Y += (float)Math.Round(moveableVelocity.DirectionVector.Y * moveableVelocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private void CheckAndResolveTileCollisions(TransformComponent moveableTransform, ColliderComponent moveableCollider)
        {
            _bounaries = _tileBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableCollider.Bounds);

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

            _bounaries.ForEach(boundary =>
            {
                if (!moveableCollider.Bounds.Intersects(boundary))
                {
                    return;
                }

                topLeft = new Rectangle(moveableCollider.Bounds.Left, moveableCollider.Bounds.Top, GameSettings.TileSize / 2, GameSettings.TileSize / 2);
                topRight = new Rectangle(moveableCollider.Bounds.Right - GameSettings.TileSize / 2, moveableCollider.Bounds.Top, GameSettings.TileSize / 2, GameSettings.TileSize / 2);

                bottomLeft = new Rectangle(moveableCollider.Bounds.Left, moveableCollider.Bounds.Bottom - GameSettings.TileSize / 2, GameSettings.TileSize / 2, GameSettings.TileSize / 2);
                bottomRight = new Rectangle(moveableCollider.Bounds.Right - GameSettings.TileSize / 2, moveableCollider.Bounds.Bottom - GameSettings.TileSize / 2, GameSettings.TileSize / 2, GameSettings.TileSize / 2);

                topLeftCollides = topLeft.Intersects(boundary) ? true : topLeftCollides;
                topRightCollides = topRight.Intersects(boundary) ? true : topRightCollides;

                bottomLeftCollides = bottomLeft.Intersects(boundary) ? true : bottomLeftCollides;
                bottomRightCollides = bottomRight.Intersects(boundary) ? true : bottomRightCollides;

                if (topLeftCollides)
                {
                    var newTopLeftIntersection = Rectangle.Intersect(topLeft, boundary);

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
                    var newTopRightIntersection = Rectangle.Intersect(topRight, boundary);

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
                    var newBottomLeftIntersection = Rectangle.Intersect(bottomLeft, boundary);

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
                    var newBottomRightIntersection = Rectangle.Intersect(bottomRight, boundary);

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
                moveableTransform.Position.X -= 2;
                moveableTransform.Position.Y -= 2;
            }

            moveableTransform.Position.Round();
        }

        private void CheckAndResolveAttackBoxCollisions(Entity moveable, AttackBoxComponent moveableAttackBox)
        {
            _bounaries = _tileBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableAttackBox.Bounds);
            _attackBoxes = _attackBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableAttackBox.Bounds);

            _bounaries.ForEach(boundary =>
            {
                if (!moveableAttackBox.Bounds.Intersects(boundary))
                {
                    return;
                }

                EntityHandler.Remove(moveable);
            });
        }
    }
}
