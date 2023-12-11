using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.Handlers;
using Template.Entities;
using Template.Systems;
using GameEngine.Constants;

namespace Template
{
    public class GameLoop : Game
    {
        private static GraphicsDeviceManager _graphics;
        private GameEngine.Systems.Systems _systems;
        private TextureHandler _textureHandler;
        private LdtkHandler _ldtkHandler;

        public GameLoop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = GameSettings.ScreenWidth;
            _graphics.PreferredBackBufferHeight = GameSettings.ScreenHeight;
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

            _textureHandler = new TextureHandler(Content);

            _ldtkHandler = new LdtkHandler(_textureHandler);

            _ldtkHandler.Init("Level_0");

            _textureHandler.LoadGroup("StorkUp", "Entities/Stork/Up");
            _textureHandler.LoadGroup("StorkRight", "Entities/Stork/Right");
            _textureHandler.LoadGroup("StorkDown", "Entities/Stork/Down");

            new Player(_textureHandler);
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