using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameEngine.Handlers
{
    public class TextureHandler
    {
        private readonly ContentManager _content;

        private static readonly Dictionary<string, Texture2D> Entities = new();
        public static readonly Dictionary<string, Texture2D> Tiles = new();

        public TextureHandler(ContentManager content)
        {
            _content = content;
        }

        public void LoadEntity(string entityTexture)
        {
            Texture2D newTexture = _content.Load<Texture2D>(entityTexture);
            Entities.Add(entityTexture, newTexture);
        }

        public void LoadTile(string tileTexture)
        {
            Texture2D newTexture = _content.Load<Texture2D>(tileTexture);
            Tiles.Add(tileTexture, newTexture);
        }

        public static Texture2D GetEntity(string texture)
        {
            return Entities[texture];
        }

        public static Texture2D GetTile(string texture)
        {
            return Tiles[texture];
        }
    }
}
