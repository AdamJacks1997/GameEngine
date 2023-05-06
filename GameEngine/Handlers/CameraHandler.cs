using Microsoft.Xna.Framework;

namespace GameEngine.Handlers
{
    public class CameraHandler
    {
        private readonly GraphicsDeviceManager _graphics;

        public CameraHandler(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
        }

        public Matrix SetFocus(Vector2 location, int zoom)
        {
            Matrix scale = Matrix.CreateScale((float)_graphics.PreferredBackBufferWidth / 1920 * zoom,
                (float)_graphics.PreferredBackBufferHeight / 1080 * zoom, 0);

            return Matrix.CreateTranslation(-location.X, -location.Y, 1)
                   * scale
                   * Matrix.CreateTranslation(
                       _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2, 0);
        }

        public Matrix SetScale(int zoom)
        {
            Matrix scale = Matrix.CreateScale((float)_graphics.PreferredBackBufferWidth / 1920 * zoom,
                (float)_graphics.PreferredBackBufferHeight / 1080 * zoom, 0);

            return Matrix.CreateTranslation(0, 0, 1)
                   * scale
                   * Matrix.CreateTranslation(
                       0, 0, 0);
        }
    }
}
