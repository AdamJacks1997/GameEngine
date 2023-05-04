using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Core;
using GameEngine.Handlers;
using System.Collections.Generic;
using Template.States;
using System.Xml.Linq;

namespace Template
{
    public class GameLoop : Game
    {
        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TextureHandler _textureHandler;
        private StateHandler _stateManager;
        private TileHandler _tileHandler;
        private readonly Dictionary<string, Component> _states = new();

        public GameLoop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            _graphics.ApplyChanges();

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureHandler = new TextureHandler(Content);
            _tileHandler = new TileHandler();
            _stateManager = new StateHandler();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            LoadTextures();

            _tileHandler.Init("ExampleMap");

            InitializeGameStateManager();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _stateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //var cameraMatrix = Matrix.CreateTranslation(100, 0, 1)
            //* Matrix.CreateScale((float)_graphics.PreferredBackBufferWidth / 1920 * 4,
            //    (float)_graphics.PreferredBackBufferHeight / 1080 * 4, 1);

            _stateManager.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        private void LoadTextures()
        {
           _textureHandler.Load("Duck");
           _textureHandler.Load("BlueTile");
           _textureHandler.Load("GreenTile");
        }

        private void InitializeGameStateManager()
        {
            MenuState menuState = new();
            PlayState playState = new(_graphics);
            PauseState pauseState = new();

            _states.Add("menu", menuState);
            _states.Add("play", playState);
            _states.Add("pause", pauseState);

            _stateManager.Init(_states, "play");
        }
    }
}