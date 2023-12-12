using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using Template.Components;
using GameEngine.Models.ECS;

namespace Template.Entities
{
    public class Tile : Entity
    {
        public Tile(
            TextureHandler textureHandler,
            Vector2 Position,
            Rectangle source)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();

            transform.Position = Position;
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Source = source;


            EntityHandler.Add(this);
        }
    }
}
