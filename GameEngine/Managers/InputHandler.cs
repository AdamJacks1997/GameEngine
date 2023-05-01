using Microsoft.Xna.Framework.Input;

namespace GameEngine.Managers
{
    public class InputHandler
    {
        public bool KeyDown(Keys key)
        {
            var test = Keyboard.GetState().IsKeyDown(key);
            return test;
        }

        public bool KeyUp(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }
    }
}