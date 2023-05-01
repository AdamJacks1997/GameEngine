using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using Template.Assets;
using System;
using GameEngine.Managers;
using Microsoft.Xna.Framework.Input;

namespace Template.Entities
{
    public class CollideableExample : Entity
    {
        public CollideableExample()
        {
            _currentTexture = Textures.Duck;
            Location = new Vector2(250, 100);

            CollisionManager.Add(this, "collideable");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawSprite(spriteBatch);
        }
    }
}
