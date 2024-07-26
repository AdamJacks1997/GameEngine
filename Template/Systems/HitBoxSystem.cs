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
                _hurtBoxes.Remove(hitBox);

                EntityHandler.Remove(hitBoxEntity);

                hitBox = null;

                //if (hurtBox.ParentEntity == Globals.PlayerEntity)
                //{
                //    return;
                //}

                BoundaryGroups.HurtBoxBoundaryHandler.Remove(hurtBox);
                BoundaryGroups.MovableBoundaryHandler.Remove(hurtBox.ParentEntity.Collider); // this could cause an issue down the line if an entity with a hurtbox isn't movable
                _hurtBoxes.Remove(hurtBox.ParentEntity.Collider); // Unsure if I can actually do this in a ForEach - Tired Covid Adam

                EntityHandler.Remove(hurtBox.ParentEntity);
            });
        }
    }
}
