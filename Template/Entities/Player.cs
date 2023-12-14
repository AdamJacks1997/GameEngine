using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using Template.Components;
using GameEngine.Models.ECS;
using GameEngine.Constants;

namespace Template.Entities
{
    public class Player : Entity
    {
        public Player(TextureHandler textureHandler)
        {
            var transform = AddComponent<TransformComponent>();
            //var animatedSprite = AddComponent<AnimatedSpriteComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var collider = AddComponent<ColliderComponent>();
            AddComponent<PlayerControllerComponent>();
            AddComponent<CameraFollowComponent>();

            transform.Position = new Vector2(100, 100);
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.3f;

            //animatedSprite.Textures.Add("Up", textureHandler.GetGroup("StorkUp"));
            //animatedSprite.Textures.Add("Right", textureHandler.GetGroup("StorkRight"));
            //animatedSprite.Textures.Add("Down", textureHandler.GetGroup("StorkDown"));

            velocity.Speed = 100f;

            collider.Bounds = transform.Bounds;

            EntityHandler.Add(this);
        }
    }
}
