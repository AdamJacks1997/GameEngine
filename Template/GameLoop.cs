using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.Handlers;
using Template.Entities;
using Template.Systems;
using GameEngine.Constants;
using Microsoft.Xna.Framework.Graphics;
using Template.Handlers;

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

            // Create Quadtrees
            _collisionHandler = new CollisionHandler();

            _collisionHandler.Init(new Rectangle(Point.Zero, Globals.CurrentLevel.Size.ToPoint()));

            _systems = new GameEngine.Systems.Systems();
            _systems
                .Add(new InputSystem())
                .Add(new ColliderSystem(_collisionHandler))
                .Add(new MovementSystem(_collisionHandler))
                .Add(new SpriteSystem())
                .Add(new AnimatedSpriteSystem())
                .Add(new CameraFollowSystem());

            //_graphics.GraphicsDevice.Clear(Color.Transparent);

            _systems.Initialize();

            new PlayerEntity(_textureHandler);

            new EnemyEntity(_textureHandler, new Vector2(1, 0));

            //for (int i = 0; i < 150; i++) // Efficiency test? Idk
            //{
            //    new EnemyEntity(_textureHandler, new Vector2(1, 0));
            //    new EnemyEntity(_textureHandler, new Vector2(-1, 0));
            //    new EnemyEntity(_textureHandler, new Vector2(0, 1));
            //    new EnemyEntity(_textureHandler, new Vector2(0, -1));
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
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            //var position = Matrix.CreateTranslation(
            //                      -Globals.CameraPosition.X - (GameSettings.TileSize / 2),
            //                      -Globals.CameraPosition.Y - (GameSettings.TileSize / 2),
            //                      0);

            //var cameraX = -Globals.CameraPosition.X - (GameSettings.TileSize / 2);
            //var cameraY = -Globals.CameraPosition.Y - (GameSettings.TileSize / 2);
            var cameraX = (GameSettings.NativeSize.X / 2) - Globals.CameraPosition.X;
            var cameraY = (GameSettings.NativeSize.Y / 2) - Globals.CameraPosition.Y;

            cameraX = MathHelper.Clamp(cameraX, -Globals.CurrentLevel.Size.X + GameSettings.NativeSize.X, 0);
            cameraY = MathHelper.Clamp(cameraY, -Globals.CurrentLevel.Size.Y + GameSettings.NativeSize.Y, 0);

            var translation = Matrix.CreateTranslation(cameraX, cameraY, 0f);

            //var offset = Matrix.CreateTranslation(
            //GameSettings.NativeSize.X / 2,
            //GameSettings.NativeSize.Y / 2,
            //                    0);

            //var transform = position * offset;

            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);

            //_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
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