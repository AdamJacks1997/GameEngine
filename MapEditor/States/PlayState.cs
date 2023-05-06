using GameEngine.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Core;
using GameEngine.Handlers;
using MapEditor.Entities;
using GameEngine.Renderers;
using MapEditor.UI;

namespace MapEditor.States
{
    internal class PlayState : Component
    {
        private readonly Player _player;
        private readonly TileSelect _tileList;
        private readonly GraphicsDeviceManager _graphics;
        private readonly CameraHandler _cameraHandler;

        public PlayState(GraphicsDeviceManager graphics)
        {
            _player = new Player();
            _tileList = new TileSelect();
            _graphics = graphics;
            _cameraHandler = new CameraHandler(_graphics);
        }

        public override void Update(GameTime gameTime)
        {
            _player.Update(gameTime);

            _tileList.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var cameraMatrix = _cameraHandler.SetFocus(_player.Location, 2);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cameraMatrix);

            TileHandler.Draw(spriteBatch);
            _player.Draw(spriteBatch);

            spriteBatch.End();

            var scaleMatrix = _cameraHandler.SetScale(2);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, scaleMatrix);

            _tileList.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
