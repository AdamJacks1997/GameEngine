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
    //public class ColliderSystem : IUpdateSystem
    //{
    //    private List<Entity> _moveables;
    //    private List<ColliderComponent> _tiles;

    //    private readonly List<Type> _moveableColliderComponentTypes = new List<Type>()
    //    {
    //        typeof(TransformComponent),
    //        typeof(VelocityComponent),
    //        typeof(ColliderComponent),
    //    };

    //    public void Update(GameTime gameTime)
    //    {
    //        _moveables = EntityHandler.GetWithComponents(_moveableColliderComponentTypes);

    //        _moveables.ForEach(moveable =>
    //        {
    //            var moveableVelocity = moveable.GetComponent<VelocityComponent>();

    //            if (moveableVelocity.DirectionVector == Vector2.Zero)
    //            {
    //                return;
    //            }

    //            var moveableTransform = moveable.GetComponent<TransformComponent>();
    //            var moveableCollider = moveable.GetComponent<ColliderComponent>();

    //            MoveWithCollisionCheck(moveableTransform, moveableVelocity, moveableCollider, gameTime);
    //        });
    //    }

    //    private void MoveWithCollisionCheck(TransformComponent moveableTransform, VelocityComponent moveableVelocity, ColliderComponent moveableCollider, GameTime gameTime)
    //    {
    //        var tempMovementVector = Vector2.Zero;

    //        if (moveableVelocity.DirectionVector.X != 0)
    //        {
    //            _tiles = BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableCollider.Bounds);

    //            _tiles.ForEach(collider =>
    //            {
    //                if (!moveableCollider.Bounds.Intersects(collider.Bounds))
    //                {
    //                    return;
    //                }

    //                var intersection = Rectangle.Intersect(moveableCollider.Bounds, collider.Bounds);

    //                tempMovementVector.X -= RoundToOne(intersection.Width * moveableVelocity.DirectionVector.X);
    //                //moveableTransform.Position.X -= intersection.Width * moveableVelocity.DirectionVector.X;
    //            });
    //        }

    //        if (moveableVelocity.DirectionVector.Y != 0)
    //        {
    //            _tiles = BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(moveableCollider.Bounds);

    //            _tiles.ForEach(collider =>
    //            {
    //                if (!moveableCollider.Bounds.Intersects(collider.Bounds))
    //                {
    //                    return;
    //                }

    //                var intersection = Rectangle.Intersect(moveableCollider.Bounds, collider.Bounds);

    //                tempMovementVector.Y -= RoundToOne(intersection.Height * moveableVelocity.DirectionVector.Y);
    //                //moveableTransform.Position.Y -= intersection.Height * moveableVelocity.DirectionVector.Y;
    //            });
    //        }

    //        if (moveableVelocity.DirectionVector.X != 0 && moveableVelocity.DirectionVector.Y != 0)
    //        {
    //            if (Math.Abs(tempMovementVector.X) < Math.Abs(tempMovementVector.Y))
    //            {
    //                tempMovementVector.Y = 0;
    //            }
    //            else
    //            {
    //                tempMovementVector.X = 0;
    //            }
    //        }

    //        moveableTransform.Position += tempMovementVector;

    //        moveableTransform.Position.Round();
    //    }

    //    private float RoundToOne(float number)
    //    {
    //        if (number > 0 && number < 1)
    //        {
    //            return 1f;
    //        }
    //        else if (number < 0 && number > -1)
    //        {
    //            return -1f;
    //        }
    //        else
    //        {
    //            return (float)Math.Round(number);
    //        }
    //    }
    //}
}
