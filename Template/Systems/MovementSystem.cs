using GameEngine.Constants;
using GameEngine.Handlers;
using GameEngine.Models.ECS;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Template.Components;

namespace Template.Systems
{
    public class MovementSystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(VelocityComponent),
            typeof(ColliderComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();
                var collider = entity.GetComponent<ColliderComponent>();

                transform.PreviousPosition = transform.Position;

                if (velocity.DirectionVector != Vector2.Zero)
                {
                    transform.Position += (velocity.DirectionVector * velocity.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                    collider.Bounds.X = (int)transform.Position.X;
                    collider.Bounds.Y = (int)transform.Position.Y + GameSettings.TileSize / 2;
                }
            });
        }
    }
}
