using System.Collections.Generic;
using GameEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Handlers
{
    public static class StateHandler
    {
        private static Dictionary<string, IComponent> _states = new ();
        private static string _activeState = "";

        public static void Init(Dictionary<string, IComponent> states, string activeState)
        {
            _states = states;
            _activeState = activeState;
        }

        //public static void Update(GameTime gameTime)
        //{
        //    _states[_activeState].Update(gameTime);
        //}

        //public static void Draw(SpriteBatch spriteBatch)
        //{
        //    _states[_activeState].Draw(spriteBatch);
        //}
    }
}
