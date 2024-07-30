using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;
using GameEngine.Globals;

namespace Template.Entities
{
    public class PlayerEntity : Entity
    {
        public PlayerEntity(Vector2 position)
        {
            var transform = AddComponent<TransformComponent>();
            var velocity = AddComponent<VelocityComponent>();
            //var animatedSprite = AddComponent<AnimatedSpriteComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var hurtBox = AddComponent<HurtBoxComponent>();
            var collider = AddComponent<ColliderComponent>();
            var equippedEntity = AddComponent<EquippedEntityComponent>();
            AddComponent<PlayerControllerComponent>();
            AddComponent<CameraFollowComponent>();
            AddComponent<FootComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            velocity.Speed = 100f;

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            hurtBox.Width = transform.Size.X - 4;
            hurtBox.Height = transform.Size.Y - 2;
            hurtBox.Offset = new Point(2, -(GameSettings.TileSize / 2) + 2);

            collider.Width = transform.Size.X;
            collider.Height = transform.Size.Y;
            collider.Offset = new Point(0, 0);

            equippedEntity.Entity = new WeaponEntity(this);
            equippedEntity.Offset = new Point(7, -1);

            EntityHandler.Add(this);
        }
    }
}
