using GameEngine.Constants;
using GameEngine.Enums;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Renderers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using GameEngine.Components;
using System.IO;

namespace Template.Systems
{
    public class ColliderSystem : IInitializeSystem, IUpdateSystem, IDrawSystem
    {
        private readonly CollisionHandler _collisionHandler;

        private List<Entity> _entities;

        private List<Rectangle> _boundaries = new List<Rectangle>();

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(ColliderComponent),
        };

        public ColliderSystem(CollisionHandler collisionHandler)
        {
            _collisionHandler = collisionHandler;
        }

        public void Initialize()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var collider = entity.GetComponent<ColliderComponent>();

                if (!entity.HasComponent<TileComponent>())
                {
                    return;
                }

                _boundaries.Add(collider.Bounds);
            });

            _collisionHandler.Update(_boundaries);
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
            //_entities = EntityHandler.GetWithComponents(_componentTypes);

            //_entities.ForEach(entity =>
            //{
            //    var collider = entity.GetComponent<ColliderComponent>();

            //    Globals.SpriteBatch.DrawRectangle(collider.Bounds, Color.Yellow);
            //});
        }
    }
}
