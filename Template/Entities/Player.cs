using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Components;
using GameEngine.Enums;
using GameEngine.Handlers;
using Microsoft.Xna.Framework.Input;

namespace Template.Entities
{
    public class Player : Entity
    {
        private int _internalTimer = 0;
        private int _animationSpeed = 10;
        private int _currentFrame = 0;

        public Player(TextureHandler textureHandler)
        {
            Textures.Add("Up", textureHandler.GetGroup("StorkUp"));
            Textures.Add("Right", textureHandler.GetGroup("StorkRight"));
            Textures.Add("Down", textureHandler.GetGroup("StorkDown"));

            CurrentDirectionTextures = GetDirectionTextures(Direction).ToList();

            CurrentTexture = GetDirectionTextures(Direction).First();

            Location = new Vector2(100, 100);

            CollisionHandler.Add(this, "player");
        }

        public override void Update(GameTime gameTime)
        {
            Velocity = new Vector2(0, 0);

            _internalTimer++;

            if (_internalTimer >= _animationSpeed)
            {
                NextFrame();
                _internalTimer = 0;
            }

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

        private void NextFrame()
        {
            if (_currentFrame < CurrentDirectionTextures.Count - 1)
            {
                _currentFrame++;
                CurrentTexture = CurrentDirectionTextures[_currentFrame];
            }
            else
            {
                _currentFrame = 0;
                CurrentTexture = CurrentDirectionTextures[_currentFrame];
            }
        }

        private void HandleInputs()
        {
            if (InputHandler.KeyDown(Keys.W))
            {
                VY -= 1;
                SetDirection (Direction.Up);
            }

            if (InputHandler.KeyDown(Keys.S))
            {
                VY += 1;
                SetDirection(Direction.Down);
            }

            if (InputHandler.KeyDown(Keys.A))
            {
                VX -= 1;
                SetDirection(Direction.Left);
            }

            if (InputHandler.KeyDown(Keys.D))
            {
                VX += 1;
                SetDirection(Direction.Right);
            }
        }

        private void SetDirection(Direction direction)
        {
            Direction = direction;
            CurrentDirectionTextures = GetDirectionTextures(direction).ToList();
        }
    }
}
