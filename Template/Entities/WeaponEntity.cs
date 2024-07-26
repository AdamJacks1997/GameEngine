using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using GameEngine.Globals;
using Template.Components;

namespace Template.Entities
{
    public class WeaponEntity : Entity
    {
        public WeaponEntity(Entity parentEntity)
        {
            var weapon = AddComponent<WeaponComponent>();
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            //var hitBox = AddComponent<HitBoxComponent>();

            weapon.ParentEntity = parentEntity;

            transform.Position = weapon.ParentEntity.Transform.Position;
            transform.Size = new Point(16, 16);

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Offset = new Point(0, -(GameSettings.TileSize / 2) + 1);
            sprite.Source = new Rectangle(112, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.10045f;

            //hitBox.Width = transform.Size.X - 4;
            //hitBox.Height = transform.Size.Y - 2;
            //hitBox.Offset = new Point(2, -(GameSettings.TileSize / 2) + 2);

            EntityHandler.Add(this);
        }
    }
}
