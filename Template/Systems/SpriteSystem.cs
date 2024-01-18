using System;
using System.Collections.Generic;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Components;
using GameEngine.Globals;
using Template.Components;

namespace Template.Systems
{
    public class SpriteSystem : IDrawSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(SpriteComponent)
        };
        
        public void Draw()
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var sprite = entity.GetComponent<SpriteComponent>();

                var spritePosition = transform.Position + sprite.Offset.ToVector2();

                if (entity.HasComponent<VelocityComponent>())
                {
                    spritePosition.Y -= 1;
                }

                //Globals.SpriteBatch.Draw(sprite.Texture, spritePosition,
                //    sprite.Source, Color.White, sprite.Rotation,
                //    new Vector2(0, 0), 1.0f, SpriteEffects.None, (spritePosition.Y / GameSettings.TileSize) / Globals.CurrentLevel.Size.Y + sprite.Layer);

                Globals.SpriteBatch.Draw(sprite.Texture, spritePosition,
                    sprite.Source, Color.White, sprite.Rotation,
                    new Vector2(0, 0), 1.0f, SpriteEffects.None, ((spritePosition.Y / GameSettings.TileSize) / Globals.CurrentLevel.Size.Y) + sprite.Layer);
            });
        }
    }
}
