﻿using Microsoft.Xna.Framework;
using GameEngine.Handlers;
using GameEngine.Components;
using GameEngine.Models.ECS.Core;

namespace Template.Entities
{
    public class TileEntity : Entity
    {
        public TileEntity(
            Vector2 position,
            Rectangle source,
            float layer)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            AddComponent<TileComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Source = source;
            sprite.Layer = layer;

            EntityHandler.Add(this);
        }
        public TileEntity(
            Vector2 position,
            Rectangle source,
            float layer,
            Rectangle bounds)
        {
            var transform = AddComponent<TransformComponent>();
            var sprite = AddComponent<SpriteComponent>();
            var collider = AddComponent<ColliderComponent>();
            AddComponent<TileComponent>();

            transform.Position = position;
            transform.Size = new Point(16, 16);

            sprite.Texture = TextureHandler.Get("Tiles");
            sprite.Source = source;
            sprite.Layer = layer;

            collider.Offset = new Point(0, 0);
            collider.Width = bounds.Width;
            collider.Height = bounds.Height;

            EntityHandler.Add(this);
        }
    }
}
