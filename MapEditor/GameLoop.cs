using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Core;
using GameEngine.Handlers;
using System.Collections.Generic;
using MapEditor.Entities;
using MapEditor.States;
using MapEditor.Handlers;

namespace MapEditor
{
    public class GameLoop : Game
    {
        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TextureHandler _textureHandler;
        private MouseEntity _mouse;

        private readonly Dictionary<string, Component> _states = new();

        public GameLoop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "D:/Projects/GameEngine/Template/Content/bin/DesktopGL/Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            _graphics.ApplyChanges();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureHandler = new TextureHandler(Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadTextures();

            TileHandler.Init("ExampleMap");

            InitializeGameStateManager();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.Update();

            StateHandler.Update(gameTime);

            _mouse.Update(gameTime);

            MapHandler.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            StateHandler.Draw(_spriteBatch);

            _spriteBatch.Begin();

            _mouse.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void LoadTextures()
        {
            _textureHandler.LoadEntity("Duck");
            _textureHandler.LoadEntity("Entities/Mouse");

            _textureHandler.LoadTile("Tiles/BlueTile");
            _textureHandler.LoadTile("Tiles/GreenTile");
        }

        private void InitializeGameStateManager()
        {
            _mouse = new MouseEntity();

            PlayState playState = new(_graphics);
            
            _states.Add("play", playState);

            StateHandler.Init(_states, "play");
        }
    }
}