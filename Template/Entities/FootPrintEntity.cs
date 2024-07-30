using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Template.Components;
using GameEngine.Globals;

namespace Template.Entities
{
    public class FootPrintEntity : Entity
    {
        public FootPrintEntity(
            FootComponent foot,
            Vector2 position,
            int count)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var footPrint = AddComponent<FootPrintComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Source = new Rectangle(32, 128, GameSettings.TileSize, GameSettings.TileSize);
            sprite.Layer = 0.01f;

            footPrint.Foot = foot;
            footPrint.Count = count;

            EntityHandler.Add(this);
        }
    }
}
