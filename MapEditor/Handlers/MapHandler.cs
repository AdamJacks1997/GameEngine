using System.Collections.Generic;
using System.Linq;
using GameEngine.Components;
using GameEngine.Handlers;
using MapEditor.UI;
using Microsoft.Xna.Framework;

namespace MapEditor.Handlers
{
    internal class MapHandler
    {
        public static Dictionary<Vector2, Tile> Tiles = TileHandler.GetCurrentMap().ToDictionary(key => new Vector2(key.X, key.Y), value => value);

        public static void Update()
        {
            HandleInputs();
        }

        private static void HandleInputs()
        {
            if (InputHandler.MouseLeftButtonPressed())
            {
                if (string.IsNullOrEmpty(TileSelect.SelectedTile))
                {
                    return;
                }

                var mouseLocation = new Vector2(InputHandler.MousePosition.X, InputHandler.MousePosition.Y);

                var tilePosition = TileHandler.LocationToTilePosition(mouseLocation);

                Tiles[tilePosition] = new Tile(tilePosition, TileSelect.SelectedTile); // TODO: include collision type

                TileHandler.SetCurrentMap(Tiles.Values.ToHashSet());
            }
        }
    }
}
