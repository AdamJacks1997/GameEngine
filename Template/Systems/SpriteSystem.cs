using System;
using System.Collections.Generic;
using GameEngine.Handlers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Template.Components;

namespace Template.Systems
{
    public class SpriteSystem : IUpdateSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(SpriteComponent)
        };

        public SpriteSystem(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }
        
        public void Update(GameTime gameTime) // THIS FILE IS COMPLETELY UNTESTED
        {
            var entities = EntityHandler.GetWithComponents(_componentTypes);

            _graphicsDevice.Clear(Color.Honeydew);

            _spriteBatch.Begin();

            entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var sprite = entity.GetComponent<SpriteComponent>();

                _spriteBatch.Draw(sprite.texture, transform.Position,
                    new Rectangle(0, 0, sprite.texture.Width, sprite.texture.Height), Color.White, 0,
                    new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.0f);
            });

            _spriteBatch.End();
        }
    }
}
