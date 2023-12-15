using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using Template.Components;
using GameEngine.Models.ECS;
using GameEngine.Constants;

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

            transform.Position = new Vector2(350, 500);
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.105f;

            //animatedSprite.Textures.Add("Up", textureHandler.GetGroup("StorkUp"));
            //animatedSprite.Textures.Add("Right", textureHandler.GetGroup("StorkRight"));
            //animatedSprite.Textures.Add("Down", textureHandler.GetGroup("StorkDown"));

            velocity.Speed = 100f;

            collider.Bounds = new Rectangle((int)transform.Position.X, (int)transform.Position.Y + GameSettings.TileSize / 2, GameSettings.TileSize, GameSettings.TileSize + 1);

            EntityHandler.Add(this);
        }
    }
}
