using GameEngine.Constants;
using GameEngine.Enums;
using GameEngine.Handlers;
using GameEngine.Models.ECS;
using GameEngine.Renderers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Template.Components;
using Template.Entities;

namespace Template.Systems
{
    public class ColliderSystem : IUpdateSystem, IDrawSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(ColliderComponent),
        };

        private PlayerEntity _playerEntity;

        private readonly List<Type> _playerEntityComponentTypes = new List<Type>()
        {
            typeof(PlayerControllerComponent),
            typeof(ColliderComponent),
            typeof(VelocityComponent),
            typeof(TransformComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);
            _playerEntity = (PlayerEntity)EntityHandler.GetWithComponents(_playerEntityComponentTypes).Single();

            var playerCollider = _playerEntity.GetComponent<ColliderComponent>();
            var playerVelocity = _playerEntity.GetComponent<VelocityComponent>();
            var playerTransform = _playerEntity.GetComponent<TransformComponent>();

            _entities.ForEach(entity =>
            {
                var collider = entity.GetComponent<ColliderComponent>();

                if (entity == _playerEntity)
                {
                    return;
                }

                if (playerCollider.Bounds.Intersects(collider.Bounds))
                {
                    /*
                     * MOVE THIS WHOLE DAMN CLASS INTO THE INPUT SYSTEM
                     * THERE IS CURRENT A JITTER WHEN THE PLAYER COLLIDES,
                     * THIS IS DUE TO THE MOVEMENT SYSTEM SETTING THE TRANSFORM THEN THIS CLASS SETTING THE TRANSFORM AGIAN
                     * 
                     * THE MOVEMENT SHOULD ONLY HAPPEN IF IT IS NOT GOING TO COLLIDE RATHER THAN MOVING, COLLIDING, AND RESETTING
                     */

                    var rect = Rectangle.Intersect(playerCollider.Bounds, collider.Bounds);


                    var test1 = playerVelocity.Direction;

                    switch(playerVelocity.Direction)
                    {
                        case DirectionEnum.Up:
                            playerTransform.Position.Y += rect.Height;
                            playerCollider.Bounds.Y = (int)playerTransform.Position.Y + GameSettings.TileSize / 2;
                            break;
                        case DirectionEnum.Down:
                            playerTransform.Position.Y -= rect.Height;
                            playerCollider.Bounds.Y = (int)playerTransform.Position.Y + GameSettings.TileSize / 2;
                            break;
                        case DirectionEnum.Left:
                            playerTransform.Position.X += rect.Width;
                            playerCollider.Bounds.X = (int)playerTransform.Position.X;
                            break;
                        case DirectionEnum.Right:
                            playerTransform.Position.X -= rect.Width;
                            playerCollider.Bounds.X = (int)playerTransform.Position.X;
                            break;
                    }

                    var test = "";
                }
            });
        }

        public void Draw()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);
            _playerEntity = (PlayerEntity)EntityHandler.GetWithComponents(_playerEntityComponentTypes).Single();

            _entities.ForEach(entity =>
            {
                var collider = entity.GetComponent<ColliderComponent>();

                Globals.SpriteBatch.DrawRectangle(collider.Bounds, Color.Yellow);
            });

            var playerCollider = _playerEntity.GetComponent<ColliderComponent>();

            Globals.SpriteBatch.DrawRectangle(playerCollider.Bounds, Color.Yellow);
        }
    }
}
