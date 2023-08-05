using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Core;
using GameEngine.Handlers;
using Microsoft.Xna.Framework.Content;
using Template.Entities;

namespace Template.States
{
    //internal class PlayState : Component
    //{
    //    private readonly Player _player;
    //    private readonly CollidableExample _collidableExample;
    //    private readonly CameraHandler _cameraHandler;
    //    private TextureHandler _textureHandler;

    //    public PlayState(ContentManager content, GraphicsDeviceManager graphics)
    //    {
    //        _textureHandler = new TextureHandler(content);

    //        LoadTextures();

    //        _player = new Player(_textureHandler);
    //        _collidableExample = new CollidableExample(_textureHandler);
    //        _cameraHandler = new CameraHandler(graphics);
    //    }

    //    public override void Update(GameTime gameTime)
    //    {
    //        _player.Update(gameTime);
    //        _collidableExample.Update(gameTime);

    //    }

    //    public override void Draw(SpriteBatch spriteBatch)
    //    {
    //        var cameraMatrix = _cameraHandler.SetFocus(_player.Location, 4);

    //        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, cameraMatrix);

    //        //TileHandler.Draw(spriteBatch);
    //        _player.Draw(spriteBatch);
    //        _collidableExample.Draw(spriteBatch);

    //        spriteBatch.End();
    //    }

    //    private void LoadTextures()
    //    {
    //        _textureHandler.LoadGroup("StorkUp", "Entities/Stork/Up");
    //        _textureHandler.LoadGroup("StorkRight", "Entities/Stork/Right");
    //        _textureHandler.LoadGroup("StorkDown", "Entities/Stork/Down");

    //        _textureHandler.Load("Duck");
    //    }
    //}
}
