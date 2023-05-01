using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using Template.Assets;
using System;
using GameEngine.Managers;
using Microsoft.Xna.Framework.Input;

namespace Template.Entities
{
    public class Player : Entity
    {
        private InputHandler _inputHandler;

        public Player()
        {
            _currentTexture = Textures.Duck;
            Location = new Vector2(100, 100);

            _inputHandler = new InputHandler();
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = new Vector2(0, 0);
            HandleInputs();
            Move(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            DrawSprite(spriteBatch);
        }

        private void HandleInputs()
        {
            if (_inputHandler.KeyDown(Keys.W))
            {
                VY -= 10;
            }

            if (_inputHandler.KeyDown(Keys.S))
            {
                VY += 10;
            }

            if (_inputHandler.KeyDown(Keys.A))
            {
                VX -= 10;
            }

            if (_inputHandler.KeyDown(Keys.D))
            {
                VX += 10;
            }
        }
    }
}
