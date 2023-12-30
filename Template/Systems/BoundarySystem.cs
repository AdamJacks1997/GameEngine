﻿using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using GameEngine.Renderers;
using GameEngine.Globals;

namespace Template.Systems
{
    public class BoundarySystem : IInitializeSystem, IUpdateSystem, IDrawSystem
    {
        private readonly BoundaryHandler _tileBoundaryHandler;
        private readonly BoundaryHandler _hitBoxBoundaryHandler;

        private List<Entity> _tileColliderEntities;
        private List<Entity> _hitBoxEntities;

        private readonly List<Type> _tileColliderComponents = new List<Type>()
        {
            typeof(ColliderComponent),
            typeof(TileComponent),
        };

        public BoundarySystem(BoundaryHandler tileBoundaryHandler, BoundaryHandler hitBoxBoundaryHandler)
        {
            _tileBoundaryHandler = tileBoundaryHandler;
            _hitBoxBoundaryHandler = hitBoxBoundaryHandler;
        }

        public void Initialize()
        {
            _tileColliderEntities = EntityHandler.GetWithComponents(_tileColliderComponents);

            _tileColliderEntities.ForEach(tileColliderEntity =>
            {
                var tileCollider = tileColliderEntity.GetComponent<ColliderComponent>();

                _tileBoundaryHandler.Add(tileCollider);
            });

            _hitBoxEntities = EntityHandler.GetWithComponent<HitBoxComponent>();

            _hitBoxEntities.ForEach(hitBoxEntity =>
            {
                var hitBox = hitBoxEntity.GetComponent<HitBoxComponent>();

                _hitBoxBoundaryHandler.Add(hitBox);
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
            _tileColliderEntities.ForEach(tileColliderEntity =>
            {
                var tileCollider = tileColliderEntity.GetComponent<ColliderComponent>();

                Globals.SpriteBatch.DrawRectangle(tileCollider.Bounds, Color.Yellow);
            });

            _hitBoxEntities.ForEach(hitBoxEntity =>
            {
                var hitBox = hitBoxEntity.GetComponent<HitBoxComponent>();

                Globals.SpriteBatch.DrawRectangle(hitBox.Bounds, Color.Blue);
            });
        }
    }
}