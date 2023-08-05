using System;
using System.Collections.Generic;
using GameEngine.Enums;
using GameEngine.Handlers;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Template.Components;

namespace Template.Systems
{
    public class InputSystem : IUpdateSystem
    {
        public void Update(GameTime gameTime)
        {
            var entities = EntityHandler.GetWithComponent<PlayerControllerComponent>();

            entities.ForEach(entity =>
            {
                var velocity = entity.GetComponent<VelocityComponent>();

                if (InputHandler.KeyDown(Keys.W))
                {
                    velocity.Direction.Y = -1;
                }

                if (InputHandler.KeyDown(Keys.S))
                {
                    velocity.Direction.Y = 1;
                }

                if (InputHandler.KeyDown(Keys.A))
                {
                    velocity.Direction.X = -1;
                }

                if (InputHandler.KeyDown(Keys.D))
                {
                    velocity.Direction.X = 1;
                }

                if ((InputHandler.KeyDown(Keys.A) && InputHandler.KeyDown(Keys.D))
                    || !InputHandler.KeyDown(Keys.A) && !InputHandler.KeyDown(Keys.D))
                {
                    velocity.Direction.X = 0;
                }

                if ((InputHandler.KeyDown(Keys.W) && InputHandler.KeyDown(Keys.S))
                    || !InputHandler.KeyDown(Keys.W) && !InputHandler.KeyDown(Keys.S))
                {
                    velocity.Direction.Y = 0;
                }

                if (velocity.Direction != Vector2.Zero)
                {
                    velocity.LastDirection = velocity.Direction;
                }
            });
        }
    }
}
