using System;
using System.Collections.Generic;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Template.Components;
using GameEngine.Components;

namespace Template.Systems
{
    public class InputSystem : IUpdateSystem
    {
        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(PlayerControllerComponent),
            typeof(VelocityComponent),
        };

        public void Update(GameTime gameTime)
        {
            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var velocity = entity.GetComponent<VelocityComponent>();

                if (InputHandler.KeyDown(Keys.W))
                {
                    velocity.DirectionVector.Y = -1;


                    /*
                     * Get walls collisions Vector2(Player.X, Player.Y - 1)
                     * Run  var rect = Rectangle.Intersect(playerCollider.Bounds, collider.Bounds);
                     * if rect has a height then set player position?
                     * 
                     * this may need to be done in MovementSystem but IDK
                     * I think I have everything I need here
                     * BUT THIS CLASS WOULDN'T EFFECT ENEMY AI SO MOVEMENT SYSTEM WOULD BE BETTER
                     */
                }

                if (InputHandler.KeyDown(Keys.S))
                {
                    velocity.DirectionVector.Y = 1;
                }

                if (InputHandler.KeyDown(Keys.A))
                {
                    velocity.DirectionVector.X = -1;
                }

                if (InputHandler.KeyDown(Keys.D))
                {
                    velocity.DirectionVector.X = 1;
                }

                if ((InputHandler.KeyDown(Keys.A) && InputHandler.KeyDown(Keys.D))
                    || !InputHandler.KeyDown(Keys.A) && !InputHandler.KeyDown(Keys.D))
                {
                    velocity.DirectionVector.X = 0;
                }

                if ((InputHandler.KeyDown(Keys.W) && InputHandler.KeyDown(Keys.S))
                    || !InputHandler.KeyDown(Keys.W) && !InputHandler.KeyDown(Keys.S))
                {
                    velocity.DirectionVector.Y = 0;
                }

                if (velocity.DirectionVector != Vector2.Zero)
                {
                    velocity.LastDirection = velocity.Direction;
                }
            });
        }
    }
}
