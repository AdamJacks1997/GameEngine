using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameEngine.Core;
using Template.Entities;
using System;

namespace Template.States
{
    internal class MenuState : Component
    {
        private Player _player;

        public MenuState()
        {
            _player = new Player();
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _player.Draw(spriteBatch);
        }
    }
}
