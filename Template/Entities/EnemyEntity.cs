using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Models.ECS.Core;

namespace Template.Entities
{
    public class EnemyEntity : Entity
    {
        public EnemyEntity(TextureHandler textureHandler, Vector2 direction)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var collider = AddComponent<ColliderComponent>();

            transform.Position = new Vector2(350, 500);
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.105f;

            velocity.Speed = 100f;
            velocity.DirectionVector = direction;

            collider.Offset = new Point(0, GameSettings.TileSize / 2 - 1);
            collider.Bounds = new Rectangle((int)transform.Position.X + collider.Offset.X, (int)transform.Position.Y + collider.Offset.Y, GameSettings.TileSize, GameSettings.TileSize + 2);

            EntityHandler.Add(this);
        }
    }
}
