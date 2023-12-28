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
        public MeleeAttackEntity(TextureHandler textureHandler, Vector2 direction)
        {
            var transform = AddComponent<TransformComponent>();
            var velocity = AddComponent<VelocityComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var attackBox = AddComponent<AttackBoxComponent>();
            var attack = AddComponent<AttackComponent>();

            transform.Position = new Vector2(500, 380);
            transform.Size = new Point(16, 16);

            velocity.Speed = 150f;
            velocity.DirectionVector = direction;

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(112, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            attackBox.Width = transform.Size.X - 4;
            attackBox.Height = transform.Size.Y - 2;
            attackBox.Offset = new Point(2, -(GameSettings.TileSize / 2) + 2);

            attack.LifeTimeLimit = 60;

            EntityHandler.Add(this);
        }
    }
}
