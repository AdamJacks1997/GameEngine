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
    public class MovableSpacingSystem : IUpdateSystem
    {
        private List<Entity> _moveables;
        private List<ColliderComponent> _moveableColliders;

        private readonly List<Type> _moveableComponentTypes = new List<Type>()
        {
            typeof(ColliderComponent),
            typeof(VelocityComponent)
        };

        public void Update(GameTime gameTime)
        {
            _moveables = EntityHandler.GetWithComponents(_moveableComponentTypes);

            //_moveables.ForEach(moveable =>
            //{
            //    if (moveable == Globals.PlayerEntity)
            //    {
            //        return;
            //    }

            //    var aCollider = moveable.GetComponent<ColliderComponent>();
            //    var aTransform = moveable.GetComponent<TransformComponent>();

            //    _moveableColliders = BoundaryGroups.MovableBoundaryHandler.BoundaryQuadtree.FindCollisions(aCollider.Bounds);

            //    _moveableColliders.ForEach(bCollider =>
            //    {
            //        if (bCollider.ParentEntity == Globals.PlayerEntity)
            //        {
            //            return;
            //        }

            //        if (aCollider == bCollider)
            //        {
            //            return;
            //        }

            //        var bTransform = bCollider.ParentEntity.GetComponent<TransformComponent>();

            //        var distance = Vector2.Distance(aTransform.GridPosition.ToVector2(), bTransform.GridPosition.ToVector2());

            //        if (distance < 3)
            //        {
            //            var aVelocity = moveable.GetComponent<VelocityComponent>();
            //            var bVelocity = bCollider.ParentEntity.GetComponent<VelocityComponent>();

            //            if (aTransform.Position.X >= bTransform.Position.X)
            //            {
            //                aVelocity.DirectionVector.X = 1;
            //                bVelocity.DirectionVector.X = -1;
            //            }
            //            else
            //            {
            //                aVelocity.DirectionVector.X = -1;
            //                bVelocity.DirectionVector.X = 1;
            //            }

            //            if (aTransform.Position.Y >= bTransform.Position.Y)
            //            {
            //                aVelocity.DirectionVector.Y = 1;
            //                bVelocity.DirectionVector.Y = -1;
            //            }
            //            else
            //            {
            //                aVelocity.DirectionVector.Y = -1;
            //                bVelocity.DirectionVector.Y = 1;
            //            }
            //        }
            //    });
            //});
        }
    }
}
