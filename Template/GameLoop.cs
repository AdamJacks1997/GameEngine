using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.Handlers;
using Template.Entities;
using Template.Systems;
using GameEngine.Constants;
using Microsoft.Xna.Framework.Graphics;
using Template.Handlers;
using GameEngine.Globals;
using System;

namespace Template
{
    public class GameLoop : Game
    {
        private static GraphicsDeviceManager _graphics;
        private GameEngine.Systems.Systems _systems;
        private SpriteBatch _spriteBatch;
        private TextureHandler _textureHandler;
        private LdtkHandler _ldtkHandler;
        private CollisionHandler _collisionHandler;

        private RenderTarget2D _nativeRenderTarget;

        public GameLoop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Fix framerate to 60FPS, idk if this is needed:
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = (int)GameSettings.ScreenSize.X;
            _graphics.PreferredBackBufferHeight = (int)GameSettings.ScreenSize.Y;

            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, 640, 360);

            new DirectionConstants();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = _spriteBatch;

            _textureHandler = new TextureHandler(Content);

            _textureHandler.Load("Tiles", "TinyDungeon");

            //_textureHandler.LoadGroup("StorkUp", "Entities/Stork/Up");
            //_textureHandler.LoadGroup("StorkRight", "Entities/Stork/Right");
            //_textureHandler.LoadGroup("StorkDown", "Entities/Stork/Down");

            _ldtkHandler = new LdtkHandler(_textureHandler);

            _ldtkHandler.LoadLevel(0);

            _collisionHandler = new CollisionHandler();

            _collisionHandler.Init(new Rectangle(Point.Zero, Globals.CurrentLevel.Size.ToPoint()));

            _systems = new GameEngine.Systems.Systems();

            _systems
                .Add(new InputSystem())
                .Add(new ColliderSystem(_collisionHandler))
                .Add(new EntityStateSystem(_collisionHandler))
                .Add(new TargetSystem())
                .Add(new PathControllerSystem())
                .Add(new ChaseSystem())
                .Add(new MovementSystem(_collisionHandler))
                .Add(new SpriteSystem())
                .Add(new AnimatedSpriteSystem())
                .Add(new CameraFollowSystem());

            _systems.Initialize();

            Globals.PlayerEntity = new PlayerEntity(_textureHandler);

            new MeleeEnemyEntity(_textureHandler);

            //for (int i = 0; i < 100; i++)
            //{
            //    new MeleeEnemyEntity(_textureHandler);
            //}
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _systems.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var cameraX = (GameSettings.NativeSize.X / 2) - Globals.CameraPosition.X;
            var cameraY = (GameSettings.NativeSize.Y / 2) - Globals.CameraPosition.Y;

            cameraX = MathHelper.Clamp(cameraX, -Globals.CurrentLevel.Size.X + GameSettings.NativeSize.X, 0);
            cameraY = MathHelper.Clamp(cameraY, -Globals.CurrentLevel.Size.Y + GameSettings.NativeSize.Y, 0);

            var translation = Matrix.CreateTranslation(cameraX, cameraY, 0f);

            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);

            _spriteBatch.Begin(transformMatrix: translation, samplerState: SamplerState.PointClamp, sortMode: SpriteSortMode.FrontToBack);

            _systems.Draw();

            _spriteBatch.End();

            _graphics.GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(_nativeRenderTarget, new Rectangle(0, 0, (int)(GameSettings.ScreenSize.X / GameSettings.NativeSize.X * GameSettings.NativeSize.X), (int)(GameSettings.ScreenSize.Y / GameSettings.NativeSize.Y * GameSettings.NativeSize.Y)), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}