using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Renderers;
using GameEngine.Globals;
using Template.Entities;
using Template.Components;

namespace Template.Systems
{
    public class BoundarySystem : IInitializeSystem, IUpdateSystem, IDrawSystem
    {
        private List<Entity> _tileColliderEntities;
        private List<Entity> _hitBoxEntities;

        private readonly List<Type> _tileColliderComponents = new List<Type>()
        {
            typeof(ColliderComponent),
            typeof(TileComponent),
        };

        public void Initialize()
        {
            _tileColliderEntities = EntityHandler.GetWithComponents(_tileColliderComponents);

            _tileColliderEntities.ForEach(tileColliderEntity =>
            {
                var tileCollider = tileColliderEntity.GetComponent<ColliderComponent>();

                BoundaryGroups.TileBoundaryHandler.Add(tileCollider);
            });

            _hitBoxEntities = EntityHandler.GetWithComponent<HitBoxComponent>();

            _hitBoxEntities.ForEach(hitBoxEntity =>
            {
                var hitBox = hitBoxEntity.GetComponent<HitBoxComponent>();

                BoundaryGroups.HitBoxBoundaryHandler.Add(hitBox);
            });
        }

        public void Update(GameTime gameTime)
        {
            //_entities = EntityHandler.GetWithComponents(_componentTypes);

            //_entities.ForEach(entity =>
            //{
            //    var collider = entity.GetComponent<ColliderComponent>();

            //    if (!entity.HasComponent<TileComponent>())
            //    {
            //        return;
            //    }

            //    _boundaries.Add(collider.Bounds);
            //});

            //_collisionHandler.Update(_boundaries);
        }

        public void Draw()
        {
            var meleeEnemies = EntityHandler.GetWithComponent<BrainComponent>();

            meleeEnemies.ForEach(meleeEnemy =>
            {
                var meleeEnemyCollider = meleeEnemy.GetComponent<ColliderComponent>();

                Globals.SpriteBatch.DrawRectangle(meleeEnemyCollider.Bounds, Color.Blue);
            });

            _tileColliderEntities.ForEach(tileColliderEntity =>
            {
                var tileCollider = tileColliderEntity.GetComponent<ColliderComponent>();

                Globals.SpriteBatch.DrawRectangle(tileCollider.Bounds, Color.Yellow);
            });

            _hitBoxEntities.ForEach(hitBoxEntity =>
            {
                var hitBox = hitBoxEntity.GetComponent<HitBoxComponent>();

                Globals.SpriteBatch.DrawRectangle(hitBox.Bounds, Color.Green);
            });
        }
    }
}
