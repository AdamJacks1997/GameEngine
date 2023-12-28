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
        TransformComponent transform;
        VelocityComponent velocity;
        SpriteComponent sprite;
        AttackBoxComponent attackBox;
        ColliderComponent collider;

        public PlayerEntity()
        {
            transform = AddComponent<TransformComponent>();
            velocity = AddComponent<VelocityComponent>();
            //var animatedSprite = AddComponent<AnimatedSpriteComponent>();
            sprite = AddComponent<SpriteComponent>();
            attackBox = AddComponent<AttackBoxComponent>();
            collider = AddComponent<ColliderComponent>();
            AddComponent<PlayerControllerComponent>();
            AddComponent<CameraFollowComponent>();

            transform.Position = new Vector2(535, 380);
            transform.Size = new Point(16, 16);

            velocity.Speed = 100f;

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            attackBox.Width = transform.Size.X - 4;
            attackBox.Height = transform.Size.Y - 2;
            attackBox.Offset = new Point(2, -(GameSettings.TileSize / 2) + 2);

            collider.Width = transform.Size.X;
            collider.Height = transform.Size.Y + 1;
            collider.Offset = new Point(0, 0);

            EntityHandler.Add(this);
        }
    }
}
