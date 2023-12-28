using GameEngine.Handlers;
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
        private readonly BoundaryHandler _attackBoundaryHandler;

        private List<Entity> _entities;

        private List<Rectangle> _boundaries = new List<Rectangle>();

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(ColliderComponent),
        };

        public BoundarySystem(BoundaryHandler tileBoundaryHandler, BoundaryHandler attackBoundaryHandler)
        {
            _tileBoundaryHandler = tileBoundaryHandler;
            _attackBoundaryHandler = attackBoundaryHandler;
        }

        public void Initialize()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                if (entity.HasComponent<TileComponent>())
                {
                    var collider = entity.GetComponent<ColliderComponent>();

                    _tileBoundaryHandler.Add(collider.Bounds);
                }

                if (entity.HasComponent<AttackBoxComponent>())
                {
                    var collider = entity.GetComponent<AttackBoxComponent>();

                    _attackBoundaryHandler.Add(collider.Bounds);
                }
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
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                if (entity.HasComponent<TileComponent>())
                {
                    var collider = entity.GetComponent<ColliderComponent>();

                    Globals.SpriteBatch.DrawRectangle(collider.Bounds, Color.Yellow);
                }

                if (entity.HasComponent<AttackBoxComponent>())
                {
                    var collider = entity.GetComponent<AttackBoxComponent>();

                    Globals.SpriteBatch.DrawRectangle(collider.Bounds, Color.Blue);
                }
            });
        }
    }
}
