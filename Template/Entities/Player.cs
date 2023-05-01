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

            CollisionManager.Add(this, "player");
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = new Vector2(0, 0);
            HandleInputs();

            if (CollisionManager.IsPerPixelCollision("player", "collideable"))
            {
                RenderBoundingBox = true;
            }
            else
            {
                RenderBoundingBox = false;
            }

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
                VY -= 1;
            }

            if (_inputHandler.KeyDown(Keys.S))
            {
                VY += 1;
            }

            if (_inputHandler.KeyDown(Keys.A))
            {
                VX -= 1;
            }

            if (_inputHandler.KeyDown(Keys.D))
            {
                VX += 1;
            }
        }
    }
}
