using System;
using System.Collections.Generic;
using GameEngine.Handlers;
using GameEngine.Models.ECS.Core;
using GameEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Template.Components;
using GameEngine.Components;
using GameEngine.Globals;
using Template.Entities;

namespace Template.Systems
{
    public class InputSystem : IUpdateSystem
    {
        private static MouseState _previousMouseState, _currentMouseState;
        private static KeyboardState _previousKeyboardState, _currentKeyboardState;

        private static Vector2 _mousePosition => new(_currentMouseState.X, _currentMouseState.Y);

        private List<Entity> _entities;

        private readonly List<Type> _componentTypes = new List<Type>()
        {
            typeof(PlayerControllerComponent),
            typeof(VelocityComponent),
        };

        public void Update(GameTime gameTime)
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();

            // -------------------------------------------------------------------------------

            _entities = EntityHandler.GetWithComponents(_componentTypes);

            _entities.ForEach(entity =>
            {
                var velocity = entity.GetComponent<VelocityComponent>();

                if (KeyDown(Keys.W))
                {
                    velocity.DirectionVector.Y = -1;
                }

                if (KeyDown(Keys.S))
                {
                    velocity.DirectionVector.Y = 1;
                }

                if (KeyDown(Keys.A))
                {
                    velocity.DirectionVector.X = -1;
                }

                if (KeyDown(Keys.D))
                {
                    velocity.DirectionVector.X = 1;
                }

                if ((KeyDown(Keys.A) && KeyDown(Keys.D))
                    || !KeyDown(Keys.A) && !KeyDown(Keys.D))
                {
                    velocity.DirectionVector.X = 0;
                }

                if ((KeyDown(Keys.W) && KeyDown(Keys.S))
                    || !KeyDown(Keys.W) && !KeyDown(Keys.S))
                {
                    velocity.DirectionVector.Y = 0;
                }

                if (velocity.DirectionVector != Vector2.Zero)
                {
                    velocity.LastDirection = velocity.Direction;

                    velocity.DirectionVector.Normalize();

                    if (velocity.DirectionVector.X != 0 && velocity.DirectionVector.Y != 0)
                    {
                        velocity.DirectionVector *= new Vector2(GameSettings.DiagnalSpeedMultiplier);
                    }
                }

                //if (MouseLeftButtonPressed())
                //{
                //    var transform = entity.GetComponent<TransformComponent>();

                //    var spawnPosition = transform.Position;

                //    var mouseWorldPosition = ScreenToWorld(_mousePosition);

                //    new TileEntity(mouseWorldPosition, new Rectangle(7 * 16, 7 * 16,4,4), 0.3f);

                //    Vector2 direction = -Vector2.Normalize(transform.Position - mouseWorldPosition);

                //    Vector2 offset = ((transform.Size.X / 2) + (transform.Size.Y / 2) / 2) * direction;

                //    spawnPosition += offset;

                //    new MeleeAttackEntity(spawnPosition, direction, Vector2ToRotation(Vector2.Zero));
                //}
            });
        }

        private bool KeyDown(Keys key)
        {
            var test = Keyboard.GetState().IsKeyDown(key);
            return test;
        }

        private bool KeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

        private bool KeyPressed(Keys k)
        {
            return _currentKeyboardState.IsKeyDown(k) && _previousKeyboardState.IsKeyUp(k);
        }

        private bool MouseLeftButtonPressed()
        {
            return Mouse.GetState().LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;
        }

        private bool MouseRightButtonPressed()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released;
        }

        private bool MouseLeftButtonHeld()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed;
        }

        private bool MouseRightButtonHeld()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed;
        }

        private Vector2 ScreenToWorld(Vector2 position)
        {
            var scale = new Vector2(GameSettings.NativeSize.X / GameSettings.ScreenSize.X, GameSettings.NativeSize.Y / GameSettings.ScreenSize.Y);

            return Globals.CameraPosition + (_mousePosition * scale);
        }

        private float Vector2ToRotation(Vector2 direction)
        {
            // Calculate the angle in radians using Math.Atan2
            float angleRadians = (float)Math.Atan2(direction.Y, direction.X);

            // Ensure the angle is positive (between 0 and 2 * PI)
            if (angleRadians < 0)
            {
                angleRadians += MathHelper.TwoPi; // Add 2 * PI to get a positive angle
            }

            // Convert the angle to degrees if needed
            float angleDegrees = MathHelper.ToDegrees(angleRadians);

            // Now 'angleRadians' contains the angle in radians and 'angleDegrees' in degrees

            return angleDegrees;
        }
    }
}
