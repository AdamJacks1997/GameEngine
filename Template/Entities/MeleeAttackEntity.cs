using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;
using System;
using GameEngine.Globals;

namespace Template.Entities
{
    public class MeleeAttackEntity : Entity
    {
        public MeleeAttackEntity(Vector2 position, Vector2 direction, float rotation)
        {
            var transform = AddComponent<TransformComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var hitBox = AddComponent<HitBoxComponent>();
            var attack = AddComponent<AttackComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            velocity.Speed = 150f;
            velocity.DirectionVector = direction;

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(112, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;
            sprite.Rotation = rotation;

            hitBox.Width = transform.Size.X - 4;
            hitBox.Height = transform.Size.Y - 2;
            hitBox.Offset = new Point(2, -(GameSettings.TileSize / 2) + 2);

            attack.LifeTimeLimit = 60;

            EntityHandler.Add(this);
        }
    }
}
