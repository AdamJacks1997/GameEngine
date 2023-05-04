using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Components;
using Microsoft.Xna.Framework;

namespace GameEngine.Handlers
{
    public class CameraHandler
    {
        private GraphicsDeviceManager _graphics;

        private Matrix _scale;

        public CameraHandler(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            _scale = Matrix.CreateScale((float)_graphics.PreferredBackBufferWidth / 1920 * 4,
                (float)_graphics.PreferredBackBufferHeight / 1080 * 4, 1);
        }

        public Matrix SetFocus(Vector2 location)
        {

            return Matrix.CreateTranslation(-location.X, -location.Y, 1)
                   * _scale
                   * Matrix.CreateTranslation(
                       _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2, 0);
        }
    }
}
