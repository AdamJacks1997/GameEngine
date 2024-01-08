using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using Template.Components;
using GameEngine.Enums;
using GameEngine.Globals;

namespace Template.Systems
{
    public class EntityStateSystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private Rectangle _losBoundary = new Rectangle();

        private List<ColliderComponent> _boundaries;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(BrainComponent),
            typeof(TransformComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();
                var brain = entity.GetComponent<BrainComponent>();
                var collider = entity.GetComponent<ColliderComponent>();

                var targetEntity = brain.Target;

                var targetTranform = targetEntity.GetComponent<TransformComponent>();
                var targetCollider = targetEntity.GetComponent<ColliderComponent>();

                var distanceFromEntityToTarget = Vector2.Distance(transform.GridPosition.ToVector2(), targetTranform.GridPosition.ToVector2());

                bool hasLineOfSight = false;

                if (brain.LineOfSightMaxDistance > distanceFromEntityToTarget)
                {
                    hasLineOfSight = HasLineOfSight(distanceFromEntityToTarget, brain.LineOfSightMaxDistance, transform, targetTranform, collider);
                }

                // Wander -- Entity is outside of Player range
                if (!hasLineOfSight && brain.State != EntityStateEnum.Wander && brain.PathStartDistance < distanceFromEntityToTarget)
                {
                    brain.State = EntityStateEnum.Wander;
                    velocity.DirectionVector = Vector2.Zero;
                }

                // FollowPath -- Entity is inside of Player Maximum Range
                if (!hasLineOfSight && brain.State != EntityStateEnum.FollowPath && brain.PathStartDistance >= distanceFromEntityToTarget)
                {
                    brain.State = EntityStateEnum.FollowPath;
                    velocity.DirectionVector = Vector2.Zero;
                }

                // RangeAttack -- Entity has line of sight AND IS RANGED

                // Chase -- Entity has line of sight AND IS MELEE
                if (hasLineOfSight && brain.EntityType == EntityTypeEnum.Melee && brain.AttackDistance < distanceFromEntityToTarget)
                {
                    brain.State = EntityStateEnum.Chase;
                    //velocity.DirectionVector = Vector2.Zero;
                }

                // if is in distance of player (stored in melee component
                // send ray
                // get closest collided tile/player
                // if closest collidable is tile then NO line of sight
                // if closest if player then YES line of sight
                //if ()

                // Melee -- Entity is within melee range of Player AND IS MELEE

                if (brain.AttackDistance >= distanceFromEntityToTarget)
                {
                    brain.State = EntityStateEnum.MeleeAttack;
                    velocity.DirectionVector = Vector2.Zero;
                }
            });
        }

        private bool HasLineOfSight(float distanceFromEntityToTarget, float LineOfSightMaxDistance, TransformComponent transform, TransformComponent targetTransform, ColliderComponent collider)
        {
            var hasLineOfSight = true;

            var stepDistance = (distanceFromEntityToTarget / LineOfSightMaxDistance) * GameSettings.TileSize;
            Vector2 direction = Vector2.Normalize(transform.Position - targetTransform.Position);

            _losBoundary = collider.Bounds;

            for (int i = 0; i < LineOfSightMaxDistance - 1; i++)
            {
                _losBoundary.Location += new Point((int)Math.Round(stepDistance * -direction.X), (int)Math.Round(stepDistance * -direction.Y));

                _boundaries = BoundaryGroups.TileBoundaryHandler.BoundaryQuadtree.FindCollisions(_losBoundary);

                _boundaries.ForEach(_boundary =>
                {

                    if (!_losBoundary.Intersects(_boundary.Bounds))
                    {
                        return;
                    }

                    hasLineOfSight = false; // if this is never ran, hasLineOfSight = true
                });
            }

            return hasLineOfSight;
        }
    }
}
