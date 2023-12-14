using System;
using System.Collections.Generic;
using GameEngine.Constants;
using GameEngine.Handlers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Template.Components;

namespace Template.Systems
{
    public class SpriteSystem : IDrawSystem
    {
        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(TransformComponent),
            typeof(SpriteComponent)
        };
        
        public void Draw()
        {
            var entities = EntityHandler.GetWithComponents(_componentTypes);

            entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var sprite = entity.GetComponent<SpriteComponent>();

                Globals.SpriteBatch.Draw(sprite.Texture, transform.Position,
                    sprite.Source, Color.White, 0,
                    new Vector2(0, 0), 1.0f, SpriteEffects.None, sprite.Layer);
            });
        }
    }
}
