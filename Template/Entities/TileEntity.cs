using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;

namespace Template.Entities
{
    public class TileEntity : Entity
    {
        public TileEntity(
            TextureHandler textureHandler,
            Vector2 position,
            Rectangle source,
            float layer)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Source = source;
            sprite.Layer = layer;


            EntityHandler.Add(this);
        }
        public TileEntity(
            TextureHandler textureHandler,
            Vector2 position,
            Rectangle source,
            float layer,
            Rectangle bounds
            )
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var collider = AddComponent<ColliderComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            sprite.Texture = textureHandler.Get("Tiles");
            sprite.Source = source;
            sprite.Layer = layer;

            collider.Bounds = bounds;

            EntityHandler.Add(this);
        }
    }
}
