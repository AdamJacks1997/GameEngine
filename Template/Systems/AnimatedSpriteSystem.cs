using System;
using System.Collections.Generic;
using GameEngine.Core;
using GameEngine.Enums;
using GameEngine.Handlers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Template.Components;

namespace Template.Systems
{
    public class AnimatedSpriteSystem : IUpdateSystem
    {
        private readonly GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;

        private readonly Dictionary<Vector2, string> _directionDictionary = new()
        {
            { new Vector2(1,0), "Right" },
            { new Vector2(-1,0), "Right" },
            { new Vector2(0,1), "Down" },
            { new Vector2(0,-1), "Up" },

            { new Vector2(1,1), "Down" },
            { new Vector2(-1,1), "Down" },
            { new Vector2(1,-1), "Up" },
            { new Vector2(-1,-1), "Up" }
        };

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(AnimatedSpriteComponent),
            typeof(VelocityComponent)
        };

        public AnimatedSpriteSystem(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }
        
        public void Update(GameTime gameTime)
        {
            var entities = EntityHandler.GetWithComponents(_componentTypes);

            _graphicsDevice.Clear(Color.Honeydew);

            _spriteBatch.Begin();

            entities.ForEach(entity =>
            {
                var transform = entity.GetComponent<TransformComponent>();
                var velocity = entity.GetComponent<VelocityComponent>();
                var animatedSprite = entity.GetComponent<AnimatedSpriteComponent>();

                var directionTextures = animatedSprite.Textures[_directionDictionary[velocity.LastDirection]];

                if (animatedSprite.Play)
                {
                    animatedSprite.InternalTimer++;

                    if (animatedSprite.InternalTimer >= animatedSprite.AnimationSpeed)
                    {
                        animatedSprite = NextFrame(animatedSprite, directionTextures);
                        animatedSprite.InternalTimer = 0;
                    }
                }

                animatedSprite.CurrentTexture = directionTextures[animatedSprite.CurrentFrame];

                SpriteEffects effect = velocity.LastDirection == new Vector2(-1, 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

                _spriteBatch.Draw(animatedSprite.CurrentTexture, transform.Position,
                    new Rectangle(0, 0, animatedSprite.CurrentTexture.Width, animatedSprite.CurrentTexture.Height), Color.White, 0,
                    new Vector2(0, 0), 1.0f, effect, 0.0f);
            });

            _spriteBatch.End();
        }

        private AnimatedSpriteComponent NextFrame(AnimatedSpriteComponent animatedSprite, List<Texture2D> textures)
        {
            if (animatedSprite.CurrentFrame < textures.Count - 1)
            {
                animatedSprite.CurrentFrame++;
                animatedSprite.CurrentTexture = textures[animatedSprite.CurrentFrame];
            }
            else
            {
                animatedSprite.CurrentFrame = 0;
                animatedSprite.CurrentTexture = textures[animatedSprite.CurrentFrame];
            }

            return animatedSprite;
        }
    }
}
