using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameEngine.Handlers;
using Template.Entities;
using Template.Systems;
using GameEngine.Constants;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

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
            _textureHandler = new TextureHandler(Content);

            _systems = new GameEngine.Systems.Systems();
            _systems
                .Add(new InputSystem())
                .Add(new MovementSystem())
                .Add(new SpriteSystem(_graphics.GraphicsDevice))
                .Add(new AnimatedSpriteSystem(_graphics.GraphicsDevice));

            _textureHandler.Load("Tiles", "TinyDungeon");

            _textureHandler.LoadGroup("StorkUp", "Entities/Stork/Up");
            _textureHandler.LoadGroup("StorkRight", "Entities/Stork/Right");
            _textureHandler.LoadGroup("StorkDown", "Entities/Stork/Down");

            //_graphics.GraphicsDevice.Clear(Color.Transparent);

            _systems.Initialize();

            new Player(_textureHandler);

            _ldtkHandler = new LdtkHandler();

            var map = _ldtkHandler.Init();

            var floor = map.Levels[0].LayerInstances.Single(li => li.Name == "Floor");

            var walls = map.Levels[0].LayerInstances.Single(li => li.Name == "Walls");

            floor.AutoLayerTiles.ForEach(tile =>
            {
                new Tile(_textureHandler, tile.PixelPosition, tile.Source);
            });

            walls.AutoLayerTiles.ForEach(tile =>
            {
                new Tile(_textureHandler, tile.PixelPosition, tile.Source);
            });
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _systems.Update(gameTime);

            base.Update(gameTime);
        }

        //protected override void Draw(GameTime gameTime)
        //{
        //    _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

        //    var spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);

        //    spriteBatch.Begin();

        //    spriteBatch.End();

        //    base.Draw(gameTime);
        //}
    }
}