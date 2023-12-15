using System;
using System.Collections.Generic;
using GameEngine.Models;
using GameEngine.Enums;
using GameEngine.Handlers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Template.Components;
using GameEngine.Constants;
using GameEngine.Models.ECS;

namespace Template.Systems
{
    public class AnimatedSpriteSystem : IUpdateSystem, IDrawSystem
    {
        //private List<Entity> _entities;

        //private readonly List<Type> _componentTypes = new List<Type>()
        //{
        //    typeof(TransformComponent),
        //    typeof(VelocityComponent),
        //    typeof(AnimatedSpriteComponent),
        //};

        public void Update(GameTime gameTime)
        {
            //    _entities = EntityHandler.GetWithComponents(_componentTypes);

            //    _entities.ForEach(entity =>
            //    {
            //        var transform = entity.GetComponent<TransformComponent>();
            //        var velocity = entity.GetComponent<VelocityComponent>();
            //        var animatedSprite = entity.GetComponent<AnimatedSpriteComponent>();

            //        //var directionTextures = animatedSprite.Textures[DirectionConstants.DirectionDictionary[velocity.LastDirection]]; // I changed the DirectionDictionary <Vector2,string> to <Vector2,Direction>

            //        //if (animatedSprite.Play)
            //        //{
            //        //    animatedSprite.InternalTimer++;

            //        //    if (animatedSprite.InternalTimer >= animatedSprite.AnimationSpeed)
            //        //    {
            //        //        animatedSprite = NextFrame(animatedSprite, directionTextures);
            //        //        animatedSprite.InternalTimer = 0;
            //        //    }
            //        //}

            //        //animatedSprite.CurrentTexture = directionTextures[animatedSprite.CurrentFrame];
            //    });
        }

    public void Draw()
        {
            //    _entities.ForEach(entity =>
            //    {
            //        var transform = entity.GetComponent<TransformComponent>();
            //        var velocity = entity.GetComponent<VelocityComponent>();
            //        var animatedSprite = entity.GetComponent<AnimatedSpriteComponent>();

            //        SpriteEffects effect = velocity.LastDirection == new Vector2(-1, 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            //        Globals.SpriteBatch.Draw(animatedSprite.CurrentTexture, transform.Position,
            //            new Rectangle(0, 0, animatedSprite.CurrentTexture.Width, animatedSprite.CurrentTexture.Height), Color.White, 0,
            //            new Vector2(0, 0), 1.0f, effect, 0.0f);
            //    });
        }

        //private AnimatedSpriteComponent NextFrame(AnimatedSpriteComponent animatedSprite, List<Texture2D> textures)
        //{
        //    if (animatedSprite.CurrentFrame < textures.Count - 1)
        //    {
        //        animatedSprite.CurrentFrame++;
        //        animatedSprite.CurrentTexture = textures[animatedSprite.CurrentFrame];
        //    }
        //    else
        //    {
        //        animatedSprite.CurrentFrame = 0;
        //        animatedSprite.CurrentTexture = textures[animatedSprite.CurrentFrame];
        //    }

        //    return animatedSprite;
        //}
    }
}
