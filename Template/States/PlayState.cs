using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using GameEngine.Core;
using Template.Entities;

namespace Template.States
{
    internal class PlayState : Component
    {
        private Player _player;
        private CollideableExample _collideableExample;

        public PlayState()
        {
            _player = new Player();
            _collideableExample = new CollideableExample();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
            _collideableExample.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _player.Draw(spriteBatch);
            _collideableExample.Draw(spriteBatch);
        }
    }
}
