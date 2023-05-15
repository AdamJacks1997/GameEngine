using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Core;
using GameEngine.Handlers;
using System.Collections.Generic;
using Template.States;

namespace Template
{
    public class GameLoop : Game
    {
        private static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //TileHandler.Init("ExampleMap");

            InitializeGameStateManager();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.Update();

            StateHandler.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            StateHandler.Draw(_spriteBatch);

            base.Draw(gameTime);
        }

        private void InitializeGameStateManager()
        {
            MenuState menuState = new();
            PlayState playState = new(Content, _graphics);
            PauseState pauseState = new();

            _states.Add("menu", menuState);
            _states.Add("play", playState);
            _states.Add("pause", pauseState);

            StateHandler.Init(_states, "play");
        }
    }
}