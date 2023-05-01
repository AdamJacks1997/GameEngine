﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine.Core;
using GameEngine.Managers;
using System.Collections.Generic;
using Template.States;
using Template.Assets;

namespace Template
{
    public class GameLoop : Game
    {
        private static GraphicsDeviceManager Graphics;
        private SpriteBatch _spriteBatch;
        private Textures _textures;
        private StateManager _stateManager;
        private readonly Dictionary<string, Component> _states = new();

        public GameLoop()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            Graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
            Graphics.ApplyChanges();

            _textures = new Textures();
            _stateManager = new StateManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _textures.LoadContent(Content);
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

            _spriteBatch.Begin();

            _stateManager.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitializeGameStateManager()
        {
            MenuState menuState = new();
            PlayState playState = new();
            PauseState pauseState = new();

            _states.Add("menu", menuState);
            _states.Add("play", playState);
            _states.Add("pause", pauseState);

            _stateManager.Init(_states, "menu");
        }
    }
}