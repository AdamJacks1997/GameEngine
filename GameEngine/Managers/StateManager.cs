using System.Collections.Generic;
using GameEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Managers
{
    public class StateManager : Component
    {
        private Dictionary<string, Component> _states = new ();
        private string _activeState = "";

        public void Init(Dictionary<string, Component> states, string activeState)
        {
            _states = states;
            _activeState = activeState;
        }

        public override void Update(GameTime gameTime)
        {
            _states[_activeState].Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _states[_activeState].Draw(spriteBatch);
        }
    }
}
