using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Globals;
using Template.Components;
using System.Diagnostics;

namespace Template.Systems
{
    public class HitBoxSystem : IUpdateSystem
    {
        private List<Entity> _hitBoxEntities;
        private List<ColliderComponent> _tiles;
        private List<ColliderComponent> _hurtBoxes;

        private readonly List<Type> _hitBoxComponentTypes = new List<Type>()
        {
            typeof(HitBoxComponent),
        };

        public void Update(GameTime gameTime)
        {
            _hitBoxEntities = EntityHandler.GetWithComponents(_hitBoxComponentTypes);

            _hitBoxEntities.ForEach(hitBoxEntity =>
            {
                CheckAndResolveHitBoxCollisions(hitBoxEntity);
            });
        }

        private void CheckAndResolveHitBoxCollisions(Entity hitBoxEntity)
        {
            var hitBox = hitBoxEntity.GetComponent<HitBoxComponent>();

            if (hitBox.ParentEntity == null)
            {
                return;
            }

            _tiles = BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(hitBox.Bounds);
            _hurtBoxes = BoundaryGroups.HurtBoxBoundaryHandler.BoundaryQuadtree.FindCollisions(hitBox.Bounds);

            _tiles.ForEach(collider =>
            {
                if (hitBox == null)
                {
                    return;
                }

                if (!hitBox.Bounds.Intersects(collider.Bounds))
                {
                    return;
                }

                BoundaryGroups.HitBoxBoundaryHandler.Remove(hitBox);

                EntityHandler.Remove(hitBoxEntity);

                hitBox = null;
            });

            if (hitBox == null)
            {
                return;
            }

            _hurtBoxes.ForEach(hurtBox =>
            {
                if (hitBox == null)
                {
                    return;
                }

                if (!hitBox.Bounds.Intersects(hurtBox.Bounds))
                {
                    return;
                }

                if (hitBox.Bounds == hurtBox.Bounds)
                {
                    return;
                }

                if (hurtBox.ParentEntity == hitBox.ParentEntity.GetComponent<AttackComponent>().Owner)
                {
                    return;
                }

                BoundaryGroups.HitBoxBoundaryHandler.Remove(hitBox);

                EntityHandler.Remove(hitBoxEntity);

                hitBox = null;

                var isRemoved = BoundaryGroups.HurtBoxBoundaryHandler.Remove(hurtBox);
                BoundaryGroups.MovableBoundaryHandler.Remove(hurtBox.ParentEntity.Collider); // this could cause an issue down the line if an entity with a hurtbox isn't movable

                EntityHandler.Remove(hurtBox.ParentEntity);
            });
        }
    }
}
