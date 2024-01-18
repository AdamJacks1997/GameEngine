using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Renderers;
using GameEngine.Globals;
using Template.Components;

namespace Template.Systems
{
    public class BoundarySystem : IInitializeSystem, IUpdateSystem, IDrawSystem
    {
        private List<Entity> _tileColliderEntities;
        private List<Entity> _movableColliderEntities;
        private List<Entity> _hitBoxEntities;
        private List<Entity> _hurtBoxEntities;

        private readonly List<Type> _tileColliderComponents = new List<Type>()
        {
            typeof(ColliderComponent),
            typeof(TileComponent),
        };

        private readonly List<Type> _movableColliderComponents = new List<Type>()
        {
            typeof(ColliderComponent),
            typeof(VelocityComponent),
        };

        private readonly List<Type> _hitBoxComponents = new List<Type>()
        {
            typeof(HitBoxComponent),
        };

        private readonly List<Type> _hurtBoxComponents = new List<Type>()
        {
            typeof(HurtBoxComponent),
        };

        public void Initialize()
        {
            BoundaryGroups.TileBoundaryHandler = new BoundaryHandler();
            BoundaryGroups.MovableBoundaryHandler = new BoundaryHandler();
            BoundaryGroups.HitBoxBoundaryHandler = new BoundaryHandler();
            BoundaryGroups.HurtBoxBoundaryHandler = new BoundaryHandler();

            BoundaryGroups.TileBoundaryHandler.Init(new Rectangle(Point.Zero, Globals.CurrentLevel.Size.ToPoint()));
            BoundaryGroups.MovableBoundaryHandler.Init(new Rectangle(Point.Zero, Globals.CurrentLevel.Size.ToPoint()));
            BoundaryGroups.HitBoxBoundaryHandler.Init(new Rectangle(Point.Zero, Globals.CurrentLevel.Size.ToPoint()));
            BoundaryGroups.HurtBoxBoundaryHandler.Init(new Rectangle(Point.Zero, Globals.CurrentLevel.Size.ToPoint()));

            _tileColliderEntities = EntityHandler.GetWithComponents(_tileColliderComponents);

            _tileColliderEntities.ForEach(tileColliderEntity =>
            {
                var tileCollider = tileColliderEntity.GetComponent<ColliderComponent>();

                BoundaryGroups.TileBoundaryHandler.Add(tileCollider);
            });

            _movableColliderEntities = EntityHandler.GetWithComponents(_movableColliderComponents);

            _movableColliderEntities.ForEach(movableColliderEntity =>
            {
                var movableCollider = movableColliderEntity.GetComponent<ColliderComponent>();

                BoundaryGroups.MovableBoundaryHandler.Add(movableCollider);
            });

            _hitBoxEntities = EntityHandler.GetWithComponents(_hitBoxComponents);

            _hitBoxEntities.ForEach(hitBoxEntity =>
            {
                var hitBox = hitBoxEntity.GetComponent<HitBoxComponent>();

                BoundaryGroups.HitBoxBoundaryHandler.Add(hitBox);
            });

            _hurtBoxEntities = EntityHandler.GetWithComponents(_hurtBoxComponents);

            _hurtBoxEntities.ForEach(hurtBoxEntity =>
            {
                var hurtBox = hurtBoxEntity.GetComponent<HurtBoxComponent>();

                BoundaryGroups.HurtBoxBoundaryHandler.Add(hurtBox);
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

                //Globals.SpriteBatch.DrawRectangle(hitBox.Bounds, Color.Green);
            });
        }
    }
}
