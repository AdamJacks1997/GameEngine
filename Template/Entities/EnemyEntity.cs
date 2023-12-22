using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Models.ECS.Core;
using Template.Components;
using Template.Handlers;
using System;
using GameEngine.Globals;

namespace Template.Entities
{
    public class EnemyEntity : Entity
    {
        public EnemyEntity(TextureHandler textureHandler)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var collider = AddComponent<ColliderComponent>();
            var pathController = AddComponent<PathControllerComponent>();

            transform.Position = new Vector2(166, 150);
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Offset = new Vector2(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            velocity.Speed = 100f;

            collider.Offset = new Point(GameSettings.TileSize / 4, 0);
            collider.Bounds = new Rectangle((int)Math.Round(transform.Position.X + collider.Offset.X), (int)Math.Round(transform.Position.Y + collider.Offset.Y), GameSettings.TileSize / 2, GameSettings.TileSize + 1);

            pathController.PathHandler = new PathHandler();
            pathController.DestinationEntity = Globals.PlayerEntity;
            pathController.PathRefreshInterval = 10;
            pathController.StopDistanceFromTarget = 1;
            pathController.MaxDistanceFromTarget = 20;

            EntityHandler.Add(this);
        }
    }
}
