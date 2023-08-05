using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.Core;
using GameEngine.Handlers;
using Template.Entities;
using Template.Systems;

namespace Template
{
    public class GameLoop : Game
    {
        private static GraphicsDeviceManager _graphics;
        private GameEngine.Systems.Systems _systems;

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _systems = new GameEngine.Systems.Systems();
            _systems
                .Add(new InputSystem())
                .Add(new MovementSystem())
                .Add(new AnimatedSpriteSystem(GraphicsDevice));

            _systems.Initialize();

            var textureHandler = new TextureHandler(Content);

            textureHandler.LoadGroup("StorkUp", "Entities/Stork/Up");
            textureHandler.LoadGroup("StorkRight", "Entities/Stork/Right");
            textureHandler.LoadGroup("StorkDown", "Entities/Stork/Down");

            new Player(textureHandler);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _systems.Update(gameTime);

            base.Update(gameTime);
        }
    }
}