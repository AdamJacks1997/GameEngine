using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;
using System;
using GameEngine.Globals;

namespace Template.Entities
{
    public class PlayerEntity : Entity
    {
        public PlayerEntity(TextureHandler textureHandler)
        {
            var transform = AddComponent<TransformComponent>();
            //var animatedSprite = AddComponent<AnimatedSpriteComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var collider = AddComponent<ColliderComponent>();
            AddComponent<PlayerControllerComponent>();
            AddComponent<CameraFollowComponent>();

            transform.Position = new Vector2(535, 380);
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Offset = new Vector2(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            velocity.Speed = 100f;

            collider.Offset = new Point(0, 0);
            collider.Bounds = new Rectangle((int)Math.Round(transform.Position.X + collider.Offset.X), (int)Math.Round(transform.Position.Y + collider.Offset.Y), GameSettings.TileSize, GameSettings.TileSize + 1);

            EntityHandler.Add(this);
        }
    }
}
