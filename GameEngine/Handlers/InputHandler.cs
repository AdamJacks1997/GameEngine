using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Handlers
{
    public static class InputHandler
    {
        private static MouseState _previousMouseState, _currentMouseState;
        private static KeyboardState _previousKeyboardState, _currentKeyboardState;

        public static Vector2 MousePosition => new (_currentMouseState.X, _currentMouseState.Y);

        public static void Update()
        {
            _previousMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            _previousKeyboardState = _currentKeyboardState;
            _currentKeyboardState = Keyboard.GetState();
        }

        public static bool KeyDown(Keys key)
        {
            var test = Keyboard.GetState().IsKeyDown(key);
            return test;
        }

        public static bool KeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

        public static bool KeyPressed(Keys k)
        {
            return _currentKeyboardState.IsKeyDown(k) && _previousKeyboardState.IsKeyUp(k);
        }
        
        public static bool MouseLeftButtonPressed()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;
        }

        public static bool MouseRightButtonPressed()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed && _previousMouseState.RightButton == ButtonState.Released;
        }

        public static bool MouseLeftButtonHeld()
        {
            return _currentMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool MouseRightButtonHeld()
        {
            return _currentMouseState.RightButton == ButtonState.Pressed;
        }
    }
}