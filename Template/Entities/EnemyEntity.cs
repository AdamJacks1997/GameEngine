using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Constants;
using GameEngine.Models.ECS.Core;

namespace Template.Entities
{
    public class EnemyEntity : Entity
    {
        public EnemyEntity(TextureHandler textureHandler)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var velocity = AddComponent<VelocityComponent>();

            transform.Position = new Vector2(350, 500);
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.105f;

            velocity.Speed = 100f;

            EntityHandler.Add(this);
        }
    }
}
