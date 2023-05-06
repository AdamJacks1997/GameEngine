﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Handlers;
using Microsoft.Xna.Framework.Input;

namespace Template.Entities
{
    public class Player : Entity
    {

        public Player()
        {
            CurrentTexture = TextureHandler.GetEntity("Duck");
            Location = new Vector2(100, 100);

            CollisionHandler.Add(this, "player");
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = new Vector2(0, 0);
            HandleInputs();

            if (CollisionHandler.IsPerPixelCollision("player", "collidable"))
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
            if (InputHandler.KeyDown(Keys.W))
            {
                VY -= 1;
            }

            if (InputHandler.KeyDown(Keys.S))
            {
                VY += 1;
            }

            if (InputHandler.KeyDown(Keys.A))
            {
                VX -= 1;
            }

            if (InputHandler.KeyDown(Keys.D))
            {
                VX += 1;
            }
        }
    }
}
