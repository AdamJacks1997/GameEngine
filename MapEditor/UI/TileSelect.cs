using System.Collections.Generic;
using GameEngine.Handlers;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameEngine.Core;
using MapEditor.Entities;

namespace MapEditor.UI
{
    internal class TileSelect : Component
    {

        private readonly HashSet<TileEntity> _tiles = new ();

        public static string SelectedTile;

        public TileSelect()
        {
            var currentX = 0;

            foreach (var texture in TextureHandler.Tiles.Values)
            {
                var position = new Vector2(currentX, 0);

                _tiles.Add(new TileEntity(texture, position));

                currentX += texture.Width;
            }
        }

        public override void Update(GameTime gameTime)
        {
            HandleInputs();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var tileEntity in _tiles)
            {
                tileEntity.Draw(spriteBatch);
            }
        }

        private static void HandleInputs()
        {
            if (InputHandler.MouseLeftButtonPressed())
            {
                foreach (var tile in TextureHandler.Tiles)
                {
                    if (CollisionHandler.IsCollision("mouse", tile.Key));
                    {
                        SelectedTile = tile.Key;
                    }
                }
            }
        }
    }
}
