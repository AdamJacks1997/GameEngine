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

namespace Template.Systems
{
    public class ColliderSystem : IUpdateSystem, IDrawSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(ColliderComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var collider = entity.GetComponent<ColliderComponent>();
            });
        }

        public void Draw()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var collider = entity.GetComponent<ColliderComponent>();

                Globals.SpriteBatch.DrawRectangle(collider.Bounds, Color.Yellow);
            });
        }
    }
}
